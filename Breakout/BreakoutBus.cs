using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
    private static GameEventBus eventBus;
    public static GameEventBus GetBus() {
        return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }
    }
}