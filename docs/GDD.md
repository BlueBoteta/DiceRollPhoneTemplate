# Game Design Document — *Rot & Roll*
**Platform:** Android  
**Genre:** Roguelite / Auto-Battler / Board Game  
**Tone:** Horror, Tense, Addictive  

---

## 1. Concept

You are a survivor in a collapsing zombie-infested world. You roll dice to move across a procedurally generated tile board, scavenging supplies, fighting monsters, and uncovering the story of what went wrong — tile by tile, loop by loop. Combat resolves automatically based on your build. Your only real decisions are what to pick up, what to equip, and when to use your last medkit.

The horde is always getting stronger. Your HP is always under pressure. Keep rolling.

---

## 2. Core Loop

```
Roll dice → move across tiles → survive encounters / scavenge →
build your survivor → loop the board → dread intensifies →
story unfolds → boss fight → die or escape →
upgrade bunker → run again
```

Each run is a fresh board. Each loop of the board makes enemies stronger and rewards better. The goal is to survive long enough to reach and defeat the boss.

---

## 3. Movement System

- The map is a **tile-based board**, procedurally generated every run
- The player rolls **2 dice** each turn — the result determines how many tiles they advance
- Movement between tiles is **automatic** (no manual pathing)
- Camera is **top-down isometric**, following the player
- The board **loops** — after reaching the end, you wrap back to the start
- Each loop: enemies scale up, rewards improve, pressure intensifies

---

## 4. Tile Types

| Tile | Description |
|---|---|
| **Encounter** | Triggers an auto-battle against zombies or monsters |
| **Scavenge** | Ransack a room — find equipment, scrap, or consumables |
| **Trader** | A shady survivor trading from a van — spend Scrap for gear, consumables, or Relics |
| **Safe Room** | Rest and recover — restore HP, remove debuffs, choose a consumable or a deeper rest |
| **Story Tile** | Narrated lore drop with a choice — builds the world and meta-progression |
| **Hazard Tile** | Contaminated zone — deals direct HP damage and applies Bleed for 2–3 tiles |
| **Trap** | Bear trap, tripwire — deals damage or applies a debuff |
| **SOS Signal** | Random help: airdrop loot or a survivor joins briefly |
| **Event** | Random horror-flavored event with risk/reward outcome |
| **Mini-Game** | Interactive challenge — win for Scrap, a consumable, or an HP heal; lose for minor damage or nothing |

Tiles are procedurally placed each run with weighted distribution per loop depth.

---

## 5. Combat System

Combat is fully **auto-battler** — no player input during fights.

- Outcome is determined entirely by your **stats and gear**
- Player can intervene only to use **consumables** (e.g. Medkit, Molotov) before or during
- Fight resolution is animated — you watch it play out
- Loot drops on enemy death — collect by **hold-and-drag** to inventory, or leave it

### Enemy Roster

| Enemy | Description |
|---|---|
| **Shambler** | Slow, hits hard. Basic zombie. |
| **Crawler** | Low HP, attacks first, causes Bleed |
| **Screamer** | Doesn't attack — summons 2 more enemies into the fight |
| **Bloater** | Explodes on death, deals AOE damage to player |
| **The Stalker** | Follows you across tiles — ambushes if you land near it again |
| **Infected Survivor** | Better loot drop, hits much harder |
| **The Horde** | Late-loop swarm encounter — many weak enemies at once |

---

## 6. Stats

| Stat | Description |
|---|---|
| **ATK** | Base damage output |
| **DEF** | Damage reduction |
| **HP** | Health pool — always visible, pulses red when low |
| **Crit %** | Chance to deal double damage |
| **Lifesteal %** | Heal a % of damage dealt |
| **Special Effects** | Bleed, Burn, Poison, Stun, etc. |

---

## 7. Inventory

### Gear Slots (4)
Fixed-type slots — each accepts only its category:

```
[ Weapon ] [ Armor ] [ Helmet ] [ Accessory ]
```

- Picking up a new item when a slot is full = **choose to swap or leave** (item is gone if left)
- Gear can be **upgraded** at the Trader using Scrap

### Consumable Pouch (3 slots)
Single-use items activated manually — the only direct player input outside dice rolls:

| Consumable | Effect |
|---|---|
| **Medkit** | Restore a large amount of HP |
| **Adrenaline Shot** | Remove all active debuffs and restore a small amount of HP |
| **Molotov** | Bonus damage this fight |
| **Bandage** | Remove Bleed / status effect |
| **Painkillers** | Temporary DEF boost |

Consumables drop from Scavenge tiles and Safe Rooms — always feel scarce.

---

## 8. Currency

### Scrap
- Drops from enemies, tiles, and chests
- Used for: everything at the Trader — gear, consumables, Relics, and upgrades
- The only in-run currency — manage it carefully

### Blood Samples (Rare)
- Dropped only by special enemies and bosses
- Used exclusively for: unlocking Bunker upgrades between runs
- The long-term meta-progression currency — doesn't affect in-run spending

---

## 9. Equipment Rarity Tiers

| Tier | Name |
|---|---|
| 1 | Common |
| 2 | Scarce |
| 3 | Salvaged |
| 4 | Military-Grade |
| 5 | Pre-Collapse |
| 6 | **Eternal** |

Higher tiers have stronger base stats and more special effect slots.

---

## 10. Relics

Rare items sold by the Trader at a higher Scrap cost than regular gear. No equip slot required — buying one applies its bonus immediately and permanently for the rest of the run. Simple, stackable stat boosts that reward smart Scrap management.

| Relic | Effect |
|---|---|
| **Worn Knuckles** | +15% ATK |
| **Steel Wrap** | +20% DEF |
| **Cracked Scope** | +12% Crit chance |
| **Leech Wrap** | Heal 3 HP per kill |
| **Gambler's Coin** | +1 to every dice roll |
| **Spite** | +10 ATK when below 40% HP |
| **Gas Mask** | Immune to Hazard Tile damage |
| **Last Photo** | Once per run: survive a killing blow at 1 HP |
| **Father's Ring** | Gain 10 HP at the start of each new loop |
| **Cracked Radio** | Reveal all Story Tiles on the board at run start |

Relics become available at the Trader from loop 2 onward. More powerful ones unlock at deeper loops.

---

## 11. Story Tiles

Narrated text moments that build the world. Each has a **choice** with consequence.

Examples:
- *"You find a child's drawing on the floor. A family. A house. A red sky."* → Gain a Relic, or lose max HP
- *"A radio crackles. Someone is broadcasting. You write down the coordinates."* → Unlocks a new tile type next run
- *"A locked door. You hear breathing inside."* → Open it (risk/reward) or leave it
- *"A soldier's journal. Last entry dated three weeks ago."* → Pure lore, small Scrap reward

Story Tile choices across multiple runs build toward **meta-unlocks** — new map areas, new enemies, new Relics, deeper lore.

---

## 12. Boss Encounters

One boss per run, encountered after enough loops. Each has a horror archetype.

| Boss | Description |
|---|---|
| **The Warden** | Armored riot cop turned monster — high DEF, slow |
| **The Hive Mother** | Giant Bloater that spawns Crawlers mid-fight |
| **Patient Zero** | The original infected — fast, unpredictable, causes Bleed on hit |
| **The Signal** | Tied to the radio Story Tile mystery — unlocked through narrative |

Defeating a boss ends the run with a Victory screen and bonus Blood Samples.

---

## 13. Loop Progression

| Loop | Changes |
|---|---|
| 1 | Standard board, learn the basics |
| 2+ | Relics become available at the Trader |
| 3–4 | Rarer Relics unlock, enemy variety increases |
| 5+ | Enemies hit harder, Horde tiles appear, boss unlocks |
| Every loop | Enemies scale up, reward quality improves |

---

## 14. Meta-Progression — The Bunker

Between runs, the player is in a **Bunker** — a base upgraded with scavenged materials.

| Room | Unlock Effect |
|---|---|
| **Med Bay** | Start each run with bonus HP |
| **Armory** | Better loot pool — higher chance of rare gear |
| **Radio Room** | More Story Tiles spawn per run |
| **Generator** | Gain 1 free Scrap per tile moved |
| **Wall Reinforcements** | Boss fights start with a small shield |

Bunker upgrades persist across all runs and cost Blood Samples.

---

## 15. UI / UX Notes

- **Inventory screen** feels like a worn backpack laid open — dirty, grunge aesthetic
- Item descriptions written as **journal entries** (*"My father's knife. Still sharp."*)
- Equipping items plays tactile sounds (zipper, click, scrape)
- **HP bar** always visible — pulses red when low
- Board view is isometric, dark-toned, fog-of-war reveals tiles as you approach
- Stats screen accessible anytime via a small button — shows current build snapshot

---

## 16. Addictive Design Pillars

1. **Dice dopamine** — rolling is satisfying and unpredictable
2. **Build curiosity** — players always wonder what Relic or gear combo they can find
3. **Story hooks** — narrative tiles make players want to explore to find out what happens
4. **Survival dread** — HP is always under pressure, you never feel truly safe
5. **Bunker loop** — even a failed run feels rewarding (Blood Samples → upgrades)
6. **One more roll** — the board is short enough that players always think they can make it

---

*Document version 1.2 — Relics simplified to Scrap-bought stat boosts with no equip slots. HP replaces Vitality throughout.*
