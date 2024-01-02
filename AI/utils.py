from keras.src.saving.saving_api import load_model
import os


class Utils:
    @staticmethod
    def file_exists(file_path):
        return os.path.exists(file_path)
