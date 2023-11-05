import pandas as pd
import tensorflow as tf
import numpy as np


#PRPOGRAM MA NA CELU PRZEWIDZENIE Ceny za pomoca masy i jakosci
class ModelAI:
    def __init__(self):
        self.model_init()

    def mock_data(self):
        self.data = {
            'masa': [10.5, 20.3, 15.8],
            'jakosc': ['dobra', 'średnia', 'dobra'],
            'cena': [50.0, 30.0, 45.0]
        }

        feature_dict = {
            'numeric': list(pd.DataFrame(self.data).select_dtypes(include=['float']).columns),
            'categorical': list(pd.DataFrame(self.data).select_dtypes(include=['object']).columns)
        }

        return feature_dict

    def preprocess_numeric(self, numeric_features):
        inputs = {}
        for numericField in numeric_features:
            inputs[numericField] = tf.keras.Input(shape=(1,), dtype=tf.float32, name=numericField)
        
        x = tf.keras.layers.Concatenate()(list(inputs.values()))
        norm = tf.keras.layers.Normalization()
        norm.adapt(np.array(self.data[inputs.keys()]))
        all_numeric_inputs = norm(x)
        self.preprocessed_inputs.append(all_numeric_inputs)


        return inputs

    def preprocess_categorical(self, categorical_features):
        inputs = {}
        for stringField in categorical_features:
            inputs[stringField] = tf.keras.Input(shape=(1,), name=stringField, dtype=tf.string)
         
        for name,input  in inputs.items():
            lookup = tf.keras.layers.StringLookup(vocabulary=np.unique(self.data[name]))
            one_hot = tf.keras.layers.CategoryEncoding(max_tokens=lookup.vocab_size())
            x = lookup(input)
            x = one_hot(x)
            self.preprocessed_inputs.append(x)

        return inputs

    def model_init(self):
        self.preprocessed_inputs = self.data.copy()
        self.labels = self.preprocessed_inputs.pop("cena")
        feature_dict = self.mock_data()
        numeric_inputs = self.preprocess_numeric(feature_dict['numeric'])
        categorical_inputs = self.preprocess_categorical(feature_dict['categorical'])
        all_inputs = {**numeric_inputs, **categorical_inputs}
        preprocessed_inputs = tf.keras.layers.Concatenate()(self.preprocessed_inputs)

        output = tf.keras.layers.Dense(1)(preprocessed_inputs)
        self.model = tf.keras.Model(inputs=list(all_inputs.values()), outputs=output)
        self.model.compile(optimizer='adam', loss='mean_squared_error')
        X_numerical = np.array(model_instance.data['masa']).reshape(-1, 1)
        X_categorical = np.array(model_instance.data['jakosc'])
        y = np.array(model_instance.data['cena']).reshape(-1, 1)
        model_instance.model.fit(self.preprocessed_inputs, y, epochs=100, batch_size=1)
        # Testowanie modelu na nowych danych
        new_data = {
            'masa': [18.2],
            'jakosc': ['średnia']
        }
        X_new_numerical = np.array(new_data['masa']).reshape(-1, 1)
        X_new_categorical = np.array(new_data['jakosc'])
        predicted_price = model_instance.model.predict([X_new_numerical, X_new_categorical])
        print("Przewidziana cena:", predicted_price[0][0])


if __name__ == '__main__':
    model_instance = ModelAI()








