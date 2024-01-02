import asyncio
from signalrcore.hub_connection_builder import HubConnectionBuilder
from itertools import chain
from model_handler import ModelHandler
from response.gemstone import Gemstone


class SignalClient:
    def __init__(self):
        self.model_handler = ModelHandler()
        self.hub_connection = None
        self.connected = False
        self.gemstone_objects = []
        self._connect()
        self._wait_for_connection()
        self._register_messages()

    def _connect(self):
        try:
            self.hub_connection = (
                HubConnectionBuilder()
                .with_automatic_reconnect(
                    {
                        "type": "raw",
                        "keep_alive_interval": 10,
                        "reconnect_interval": 5,
                        "max_attempts": 5,
                    }
                )
                .with_url(
                    "https://localhost:7294/WebSocketMessageHub",
                    options={"verify_ssl": False},
                )
                .build()
            )

            self.hub_connection.start()
            self.hub_connection.on_open(lambda: self._on_connected())
            print("Connected to SignalR Hub")

        except Exception as e:
            print(f"Failed to connect to the server: {e}")

    def _wait_for_connection(self):
        while not self.connected:
            pass

    def _register_messages(self):
        self.hub_connection.on("ReceiveMessage", self.get_gemstone_data)

    def send_message_after_connection(self, data):
        if self.hub_connection:
            self.hub_connection.send("SendGemstoneDetailMessage", [data])
        else:
            print("Connection not established. Unable to send message.")

    async def listen_forever(self):
        while True:
            await asyncio.sleep(0.1)

    def _on_connected(self):
        self.connected = True

    def get_gemstone_data(self, gemstoneList):
        print(gemstoneList)
        profitability_gemstone_data = []
        unnested_arr_of_gemstones = list(chain.from_iterable(gemstoneList))

        for gemstone in unnested_arr_of_gemstones:
            gemstone_dict = {
                "CurrentPrice": gemstone["CurrentPrice"],
                "DiscountPrice": gemstone["DiscountPrice"],
                "OriginalPrice": gemstone["OriginalPrice"],
                "FeedbackPercentage": gemstone["FeedbackPercentage"],
                "FeedbackScore": gemstone["FeedbackScore"],
            }

            profitability_gemstone_data.append(gemstone_dict)

        for gemstone, profitability in zip(
            unnested_arr_of_gemstones,
            self.model_handler.predict_profitability_stones(
                profitability_gemstone_data
            ),
        ):
            profitability_value = profitability.item()
            gem = Gemstone(
                ID=gemstone["Id"],
                Name=gemstone["Name"],
                Image=gemstone["Image"],
                CurrentPrice=gemstone["CurrentPrice"],
                DiscountPrice=gemstone["DiscountPrice"],
                OriginalPrice=gemstone["OriginalPrice"],
                FeedbackPercentage=gemstone["FeedbackPercentage"],
                FeedbackScore=gemstone["FeedbackScore"],
                ProfitabilityResult=profitability_value,
            )
            self.gemstone_objects.append(gem)

        self.send_message_after_connection(self.gemstone_objects)
        # cleaning array
        self.gemstone_objects = []

    def send_processed_gemstones(self, gemstones):
        if self.hub_connection:
            self.hub_connection.send("messageToReact", [gemstones])
