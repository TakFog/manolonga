package org.gamejam.gamejammultiplayerservice;

import com.fasterxml.jackson.databind.JsonNode;
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
    private final Map<Integer, Map<String, JsonNode>> stateByRound = new ConcurrentHashMap<>();

    @PostMapping(path = "/updateState/{playerId}/{roundId}", produces = "application/json")
    Map<String, JsonNode> updateState(@PathVariable String playerId, @PathVariable final int roundId, @RequestBody JsonNode statePayload) {
        Map<String, JsonNode> stringStringMap = stateByRound.computeIfAbsent(roundId, k -> new ConcurrentHashMap<>());
        stringStringMap.put(playerId, statePayload);
        stateByRound.remove(roundId - 2); // Clean up old states
        log.info("State updated for player {} in round {}: {}", playerId, roundId, stateByRound);
        return stringStringMap;
    }

    @GetMapping("/clear")
    void clear() {
        stateByRound.clear();
    }

    public static void main(String[] args) {
        SpringApplication.run(GameJamMultiplayerServiceApplication.class, args);
    }
}
