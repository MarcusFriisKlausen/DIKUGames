using DIKUArcade.Events;

namespace Breakout;
/// <summary>
/// This class is responsible for instantiating an event bus.
/// </summary>
public static class BreakoutBus {
private static GameEventBus? eventBus;
public static GameEventBus GetBus() {
    return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
    }
}
