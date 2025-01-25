package org.gamejam.gamejammultiplayerservice;

import org.slf4j.Logger;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.*;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

@SpringBootApplication
@RestController
public class GameJamMultiplayerServiceApplication {
    private static final Logger log = org.slf4j.LoggerFactory.getLogger(GameJamMultiplayerServiceApplication.class);
    private final Map<Integer, Map<String, String>> stateByPlayer = new ConcurrentHashMap<>();

    @PostMapping("/updateState/{playerId}/{roundId}")
    Map<String, String> updateState(@PathVariable String playerId, @PathVariable final int roundId, @RequestBody String statePayload) {
        Map<String, String> stringStringMap = stateByPlayer.computeIfAbsent(roundId, k -> new ConcurrentHashMap<>());
        stringStringMap.put(playerId, statePayload);
        stateByPlayer.remove(roundId - 2); // Clean up old states
        log.info("State updated for player {} in round {}: {}", playerId, roundId, stateByPlayer);
        return stringStringMap;
    }

    @GetMapping("/clear")
    void clear() {
        stateByPlayer.clear();
    }

    public static void main(String[] args) {
        SpringApplication.run(GameJamMultiplayerServiceApplication.class, args);
    }
}
