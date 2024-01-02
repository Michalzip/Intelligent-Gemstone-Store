import pandas as pd
import numpy as np
import tensorflow as tf
from tensorflow.keras import layers
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from utils import Utils
from joblib import dump, load


class ModelTraining:
    def __init__(self):
        self.scaler = StandardScaler()
        if not Utils.file_exists("./model_trained"):
            print("Training Model....")
            self.load_data()
            self.preprocess_data()
            self.build_model()
            self.train_model()

    def load_data(self):
        dataset = pd.read_csv("./model_training/train_data.csv")
        self.features = dataset.drop(columns=["ProfitabilityResult"])
        self.labels = dataset["ProfitabilityResult"]

    def preprocess_data(self):
        self.scaled_features = self.scaler.fit_transform(self.features)
        self.X_train, self.X_test, self.y_train, self.y_test = train_test_split(
            self.scaled_features, self.labels, test_size=0.2, random_state=42
        )
        self.y_train = np.array(self.y_train)
        self.y_test = np.array(self.y_test)
        dump(self.scaler, "./model_training/train_scaler.joblib")

    def build_model(self):
        self.model = tf.keras.Sequential(
            [
                layers.Dense(
                    128, activation="relu", input_shape=(self.X_train.shape[1],)
                ),
                layers.Dense(
                    32,
                    activation="relu",
                    kernel_regularizer=tf.keras.regularizers.l2(0.001),
                ),
                layers.Dense(units=1),
            ]
        )

        optimizer = tf.keras.optimizers.legacy.Adam(learning_rate=0.01)
        self.model.compile(optimizer=optimizer, loss="mean_absolute_error")

    def train_model(self, epochs=10000, batch_size=100):
        self.model.fit(
            self.X_train,
            self.y_train,
            validation_data=(self.X_test, self.y_test),
            epochs=epochs,
            verbose=2,
            validation_split=0.3,
            batch_size=batch_size,
            callbacks=[
                tf.keras.callbacks.EarlyStopping(monitor="val_loss", patience=200)
            ],
        )
        test_results = self.model.evaluate(self.X_test, self.y_test, verbose=0)
        print("Test Loss:", test_results)

        self.model.save("model_trained")

    def load_scaler(self, scaler_path="./model_training/train_scaler.joblib"):
        self.scalear = StandardScaler()
        self.scalear = load(scaler_path)
        return self.scalear
