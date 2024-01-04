from tensorflow.keras.models import load_model
from model_training.model_training import ModelTraining
import pandas as pd
import math
from joblib import dump, load


class ModelHandler:
    def __init__(self):
        self.model_training = ModelTraining()

    def predict_profitability_stones(self, data):
        model = load_model("./model_trained")
        prepared_data = self.prepare_data(data)
        dataset = pd.DataFrame(prepared_data)
        loaded_scaler = self.model_training.load_scaler()
        scaler_data = loaded_scaler.transform(dataset)

        predictions = model.predict(scaler_data)

        for pred in predictions:
            print(f"predict value: {pred}")
        return predictions


    def prepare_data(self, data):
        for item in data:
            for key, value in item.items():
                if isinstance(value, str):
                    numeric_value = float(value)
                    item[key] = numeric_value  # delete first and last letter
                elif value is None:
                    item[key] = 0
        return data
