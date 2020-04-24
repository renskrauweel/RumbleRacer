#!/usr/bin/env python

import asyncio
import websockets

async def hello(websocket, path):
    input = await websocket.recv()
    print(f"< {input}")

    response = f"Your input was: {input}"

    await websocket.send(response)
    print(f"> {response}")

start_server = websockets.serve(hello, "localhost", 8765)
print("Server starting...")
asyncio.get_event_loop().run_until_complete(start_server)
asyncio.get_event_loop().run_forever()