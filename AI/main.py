from signal_client import SignalClient
import asyncio
from model_training.model_training import ModelTraining
from model_handler import ModelHandler


async def main():
    ModelTraining()
    signal_client = SignalClient()
    await signal_client.listen_forever()


if __name__ == "__main__":
    asyncio.run(main())
