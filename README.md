# ProtonPortSync

Automatically synchronizes Proton VPN forwarded ports with qBittorrent.

## Why?

Proton VPN assigns a dynamic forwarded port when port forwarding is enabled.

When the VPN reconnects, the forwarded port may change while qBittorrent continues listening on the previous port.

This project automatically keeps qBittorrent synchronized with the current Proton VPN forwarded port.

## Features

- Detect Proton VPN forwarded port
- Detect port changes
- Update qBittorrent listening port automatically
- Verify synchronization
- Structured logging
- Lightweight background service

## Tech Stack

- .NET 10
- C#
- Clean Architecture
- Worker Service
- Dependency Injection

## Roadmap

### V1

- Proton VPN support
- qBittorrent support
- Automatic synchronization

## License

MIT
