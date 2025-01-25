package org.gamejam.gamejammultiplayerservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.*;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

@SpringBootApplication
@RestController
public class GameJamMultiplayerServiceApplication {
    private final Map<Integer, Map<String, String>> stateByPlayer = new ConcurrentHashMap<>();

    @PostMapping("/updateState/{playerId}/{roundId}")
    Map<String, String> updateState(@PathVariable String playerId, @PathVariable final int roundId, @RequestBody String statePayload) {
        Map<String, String> stringStringMap = stateByPlayer.computeIfAbsent(roundId, k -> new ConcurrentHashMap<>());
        stringStringMap.put(playerId, statePayload);
        stateByPlayer.remove(roundId - 2); // Clean up old states
        stateByPlayer.keySet().removeIf(k -> k > roundId); // Cleanup future states if we are behind (eg a new game started)
        return stringStringMap;
    }

    public static void main(String[] args) {
        SpringApplication.run(GameJamMultiplayerServiceApplication.class, args);
    }
}
