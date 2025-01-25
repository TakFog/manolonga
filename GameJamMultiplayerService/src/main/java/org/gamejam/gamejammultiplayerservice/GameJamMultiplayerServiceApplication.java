package org.gamejam.gamejammultiplayerservice;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.node.BooleanNode;
import com.fasterxml.jackson.databind.node.ObjectNode;
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
    Map<String, JsonNode> updateState(@PathVariable String playerId, @PathVariable final int roundId, @RequestBody ObjectNode statePayload) {
        Map<String, JsonNode> stringStringMap = stateByRound.computeIfAbsent(roundId, k -> new ConcurrentHashMap<>());
        stringStringMap.merge(playerId, statePayload, (oldValue, newValue) -> {
            if (oldValue.isObject() && newValue.isObject()) {
                ((ObjectNode) oldValue).setAll((ObjectNode) newValue);
                return oldValue;
            }
            return oldValue;
        });

        if (playerId.equalsIgnoreCase("monster")) {
            stringStringMap.put("hasMonster", BooleanNode.getTrue());
        } else {
            stringStringMap.put("hasChild", BooleanNode.getTrue());
        }

        stateByRound.remove(roundId - 2); // Clean up old states
        log.info("State updated for player {} in round {}: {}", playerId, roundId, stateByRound);
        return stringStringMap;
    }

    @GetMapping("/clear")
    void clear() {
        log.info("Clearing state");
        stateByRound.clear();
    }

    public static void main(String[] args) {
        SpringApplication.run(GameJamMultiplayerServiceApplication.class, args);
    }
}
