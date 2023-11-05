# import numpy as np
# import tensorflow as tf
#
# # Definicja klasy Person
# class Person:
#     def __init__(self, age, health_status, weight):
#         self.age = age
#         self.health_status = health_status
#         self.weight = weight
#
# # Przygotowanie danych do nauki modelu (fałszywe dane do celów ilustracyjnych)
# data = [
#     (18, "zdrowy", 70),
#     (45, "chory", 85),
#     (30, "umierający", 100),
# ]
#
# # Mapowanie statusów zdrowia na liczby całkowite
# health_status_mapping = {"zdrowy": 0, "chory": 1, "umierający": 2}
# encoded_health_statuses = [health_status_mapping[status] for _, status, _ in data]
#
# # Przygotowanie danych wejściowych do modelu
# ages, _, weights = zip(*data)  # Pomijamy statusy zdrowia, bo będą używane jako etykiety (labels)
#
# # Konwersja danych wejściowych na tensory numpy
# ages = np.array(ages)
# weights = np.array(weights)
#
# # Budowa modelu
# input_age = tf.keras.layers.Input(shape=(1,))
# input_weight = tf.keras.layers.Input(shape=(1,))
#
# concatenated_input = tf.keras.layers.concatenate([input_age, input_weight])
# hidden_layer = tf.keras.layers.Dense(64, activation='relu')(concatenated_input)
# output_layer = tf.keras.layers.Dense(3, activation='softmax')(hidden_layer)  # 3 kategorie stanów zdrowia
#
# model = tf.keras.models.Model(inputs=[input_age, input_weight], outputs=output_layer)
#
# # Kompilacja modelu
# model.compile(optimizer='adam', loss='sparse_categorical_crossentropy', metrics=['accuracy'])
#
# # Trenowanie modelu
# model.fit([ages, weights], np.array(encoded_health_statuses), epochs=10, batch_size=32)
#
# # Przewidywanie stanu zdrowia dla każdej osoby
# for i in range(len(ages)):
#     predicted_health_status = model.predict([np.array([ages[i]]), np.array([weights[i]])])
#     predicted_category = np.argmax(predicted_health_status)
#     print(f"Wiek: {ages[i]}, Waga: {weights[i]}, Przewidziany stan zdrowia: {predicted_category}")


import numpy as np
import pandas as pd
import tensorflow as tf

# Przygotowanie danych do nauki modelu (fałszywe dane do celów ilustracyjnych)
data = [
    (18, "zdrowy", 70),
    (120, "umierający", 55),
    (60, "chory", 200),



]

# Tworzenie DataFrame z danymi
df = pd.DataFrame(data, columns=["age", "health_status", "weight"])

# StringLookup dla kategorii zdrowia z określeniem liczby wartości spoza słownika
health_status_lookup = tf.keras.layers.StringLookup(num_oov_indices=0)
#jakie dane ma przekstalczyc na inta
health_status_lookup.adapt(df["health_status"])


# Mapowanie stringuow zdrowia na liczby całkowite
encoded_health_statuses = health_status_lookup(df["health_status"])

# Budowa modelu #! jesli mam duzo danych np float i string to wystarczy ze stworze dwa tensory typu float i string
input_age = tf.keras.layers.Input(shape=(1,))
input_weight = tf.keras.layers.Input(shape=(1,))

#polacz ze soba tensory i uzyskaj jeden tensor
concatenated_input = tf.keras.layers.concatenate([input_age, input_weight])

#concatenated_input ustaw dla Dense jako dane do przetwarzania
#ReLU jest często stosowane w warstwach ukrytych dla większości problemów uczenia maszynowego
# ze względu na swoją skuteczność i efektywność w obliczeniach.
# warstwy ukryte to warstwy, które znajdują się między warstwą wejściową a warstwą wyjściową.
#sa uzywane do uczeniach cech itd..
#warstwa obliczeniowa, przetwara dane wejsciowe
hidden_layer = tf.keras.layers.Dense(64, activation='relu')(concatenated_input)


#warstwa output
#Softmax jest uzywane do stowozania prawdowpodobienst przynaleznosci do roznyc klas
output_layer = tf.keras.layers.Dense(3, activation='softmax')(hidden_layer)  # 3 kategorie stanów zdrowia bo 3 units

#tworzy objekt modelu zdolny do treniingu
model = tf.keras.models.Model(inputs=[input_age, input_weight], outputs=output_layer)

# konfiguracja modelu
model.compile(optimizer='adam', loss='sparse_categorical_crossentropy', metrics=['accuracy'])

# wytrenuj model: arg : z jakich danych , cel do osiagniecia
model.fit([df["age"], df["weight"]], np.array(encoded_health_statuses), epochs=10, batch_size=32)

predictions = []
# Przewidywanie stanu zdrowia dla każdej osoby
for i, row in df.iterrows():
    #przewiduj na podstaie tych danych
    prediction = model.predict([np.array([row["age"]]), np.array([row["weight"]])])
    predictions.append(prediction)

health_statuses = health_status_lookup.get_vocabulary()

for index, data in enumerate(predictions[:10]):
    predicted_class_index = np.argmax(data)
    print(f"Indeks: {index}, Przewidziany stan zdrowia: {health_statuses[predicted_class_index]}, pewnosc to {data}")






