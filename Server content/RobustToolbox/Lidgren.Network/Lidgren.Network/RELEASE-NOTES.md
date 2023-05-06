# Release Notes

## Master

## 0.2.3

- Fixed data race bug with `NetUtility.ComputeSHAHash()` and resulting bugs surrounding duplicate UID calculation.
  - Removed system-global `Mutex` in peer startup that acted as an incorrect "fix" for this.
    - This fixes compatibility with Xamarin.

## 0.2.2

- `NetRandom.NextSingle()` is an override of the new `Random.NextSingle()` on .NET 6.
- Fixed compilation warnings due to usages of obsolete symbols like `WebClient`.

## 0.2.1

- Improve some NuGet package stuff (SourceLink, symbols, README)

## 0.2.0

- Remove encryption support. The entire existing encryption support was completely insecure and broken, and did not integrate with the internals of the API in any way (i.e. it would be trivial to add on top yourself).
  - See [this example](https://github.com/space-wizards/RobustToolbox/blob/de8c2c14bb7a2c130c6c3f66f2cc443b748cdd2a/Robust.Shared/Network/NetEncryption.cs) for an implementation that actually has functional security.
  - I do want to make a better encryption API (that would encrypt packets after message combining and such, to reduce per-message bandwidth overhead) but this is not currently a thing.
- `NetQueue<T>` internal buffer now expands exponentially instead of linearly (similar to `List<T>`). This avoids O(n^2) scenarios.
- Removed duplicated packet sending code between `DEBUG` and `RELEASE`. These two paths were split due to latency simulation, and only the `DEBUG` path had been maintained for new features like dual-stack IPv6.
  - This should hopefully avoid any heisenbugs that only happen on `RELEASE`.
  - Latency simlation is now always compiled in on `RELEASE`.
  - Made latency simulation code not O(n<sup>2</sup>) processing large volumes of delayed packets.
- Added override of `NetConnection.Disconnect()` that avoids sending bye messages, thus leaving the other peer in the dark.
  - Intended for testing and hilarious shenanigans. Use with care.
- Wireshark Dissector plugin (check the repo).
- Fix `AutoExpandMTU` for IPv6 sockets. It would previously throw exceptions.
  - It should properly work for dual-stack IPv6 sockets too.
- Disabled `AutoExpandMTU` on macOS: this means you get a warning on startup instead of exception spam.
  - CoreCLR doesn't support the relevant flags for IPv4 don't-fragment on macOS yet, so this never worked.
- Lowered default MTU to 1200 bytes.
  - The previous value (1408) proved too high and could cause issues on some network configurations.
  - The new value is the same minimum as QUIC, which seems like a reasonable default.
- Use fast socket optimizations for MTU expansion.

## 0.1.0

This is the first release. Changes over base Lidgren.Network:
- **Dropped .NET Framework support.** Minimum required is now .NET Standard 2.1.
- Added support for `[ReadOnly]Span<byte>` in many places.
- Fix initialization exception if loopback is the only up network interface on the system.
- Added `NetBuffer.PeekStringSize()`
- Optimize `BitsToHoldUInt[64]` to use `lzcnt` intrinsics on .NET Core >3.1.
- Minor optimizations to `NetQueue` with unmanaged types.
- Detect endianness at runtime using `BitConverter.IsLittleEndian` instead of compile directive.
- Optimized many `NetBuffer` read/write operations to use modern APIs and otherwise.
- Removed public `SingleUIntUnion` type that solely used to cast float<->uint internally.
- Optimized MTU detection code to allocate less.
- Added extra statistics to `NetPeerStatistics` and `NetConnectionStatistics`.
- Statistics are now all `long` to avoid rollover.
- Compiled with `USE_RELEASE_STATISTICS` by default.
- Added better IPv6 support to various APIs.
- Can now connect over IPv6 link-locals. Connections get rerouted to correct adapter on response receive.
- Improvements to IPv6 Dual Stack support.
- Reduced allocations in encryption code.
- Log error if sending unreliable message above MTU.
- Add support to .NET 5 `Half` on `NetBuffer`.
- Fixed encryption stuff on .NET 5.0.1 & .NET 6
- Add `NetTime.SetNow()` to synchronize any internal engine clocks you may have with Lidgren's.
- `NetBuffer` internal buffer now expands exponentially instead of linearly (similar to `List<T>`). This avoids O(n^2) scenarios when adding lots of things to a `NetBuffer`.
- Removed allocations from packet send/receive on Windows and Linux by P/Invoking native socket APIs directly instead of allocation-heavy BCL `Socket`. (BSD and macOS not currently optimized here)
- You can now port-forward non-UDP protocols with UPnP.
- Improved network interface detection (used for broadcasting and such e.g. UPnP) to be more intelligent.
- Improved UPnP to select correct IGD if multiple devices on your network implement the relevant UPnP services.
- Improve debug logging for UPnP stuff.