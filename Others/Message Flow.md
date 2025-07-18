Here's the message sending flow in order:

1.  **User A sends message:** User A's app encrypts the message and sends it to WhatsApp's servers.
2.  **Server receives & queues:** WhatsApp server receives the message, stores it in a queue for User B, and logs it for User A.
3.  **Server attempts delivery to User B:**
    * **If User B is online:** Server pushes message directly to User B's device.
    * **If User B is offline:** Server holds message in queue, triggers push notification (if applicable).
4.  **User A's device updates status (single tick):** User A sees one gray tick, confirming server receipt.
5.  **User B's device receives message:**
    * **If pushed directly:** Message appears immediately.
    * **If via push notification:** User B taps notification, app opens, message downloads from queue.
6.  **User B's device sends delivery receipt to server:** User B's device confirms message receipt.
7.  **Server updates User A's status (double tick):** Server notifies User A's device, changing the single tick to two gray ticks.
8.  **User B reads message (optional):** User B opens chat and reads message.
9.  **User B's device sends read receipt to server (optional):** If read receipts are enabled, User B's device confirms message read.
10. **Server updates User A's status (blue ticks - optional):** Server notifies User A's device, changing the two gray ticks to two blue ticks.