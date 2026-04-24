# Game Design Document — *Rot & Roll*
**Platform:** Android  
**Genre:** Roguelite / Auto-Battler / Board Game  
**Tone:** Horror, Tense, Addictive  

---

## 1. Concept

You are a survivor in a collapsing zombie-infested world. You roll dice to move across a procedurally generated tile board, scavenging supplies, fighting monsters, and uncovering the story of what went wrong — tile by tile, loop by loop. Combat resolves automatically based on your build. Your only real decisions are what to pick up, what to equip, and when to use your last medkit.

The infection is always spreading. The horde is always getting stronger. Keep rolling.

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
- Each loop: enemies scale up, rewards improve, Infection pressure increases

---

## 4. Tile Types

| Tile | Description |
|---|---|
| **Encounter** | Triggers an auto-battle against zombies or monsters |
| **Scavenge** | Ransack a room — find equipment, scrap, or consumables |
| **Trader** | A shady survivor trading from a van — spend Scrap for gear or Remnants |
| **Safe Room** | Brief rest — small heal, candles, brief story beat, slow Infection |
| **Story Tile** | Narrated lore drop with a choice — builds the world and meta-progression |
| **Infection Tile** | Stepping on it adds Infection stacks — avoid if possible |
| **Trap** | Bear trap, tripwire — deals damage or applies a debuff |
| **SOS Signal** | Random help: airdrop loot or a survivor joins briefly |
| **Event** | Random horror-flavored event with risk/reward outcome |

Tiles are procedurally placed each run with weighted distribution per loop depth.

---

## 5. Infection Meter

A persistent pressure mechanic that runs alongside the loop system.

- Starts at **0%**, caps at **100%** — reaching 100% = death
- Fills passively each loop and when stepping on Infection Tiles
- Slowed by: Medkits, Safe Rooms, certain Remnants (e.g. Gas Mask)
- Creates urgency — you cannot loop forever, you must push toward the boss

---

## 6. Combat System

Combat is fully **auto-battler** — no player input during fights.

- Outcome is determined entirely by your **stats and Remnants** (your build)
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

## 7. Stats

| Stat | Name | Description |
|---|---|---|
| ATK | **Brutality** | Base damage output |
| DEF | **Fortitude** | Damage reduction |
| HP | **Vitality** | Health pool |
| Crit Chance | **Desperation** | Higher chance when HP is low |
| Lifesteal | **Cannibalize** | Heal a % of damage dealt |
| True Damage | **Execution** | Ignores enemy Fortitude |
| Special Effects | — | Bleed, Burn, Poison, Stun, etc. |

---

## 8. Inventory

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
| **Medkit** | Restore HP |
| **Adrenaline Shot** | Reduce Infection meter |
| **Molotov** | Bonus damage this fight |
| **Bandage** | Remove Bleed / status effect |
| **Painkillers** | Temporary Fortitude boost |

Consumables drop from Scavenge tiles and Safe Rooms — always feel scarce.

---

## 9. Currency

Two currencies with distinct purposes — never enough of either.

### Scrap (Common)
- Drops from enemies, tiles, chests
- Used for: buying from Trader, upgrading gear, unlocking doors on Story Tiles
- The moment-to-moment survival currency

### Blood Samples (Rare)
- Dropped only by special enemies and bosses
- Used for: buying Remnants, unlocking Bunker upgrades between runs
- The long-term power investment currency

---

## 10. Equipment Rarity Tiers

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

## 11. Remnants (Relics)

Objects left behind by the dead. Equip them for passive effects that define your build.

| Remnant | Effect |
|---|---|
| **Bloody Bat** | Deal bonus Brutality when Infected |
| **Gas Mask** | Slow Infection gain |
| **Last Photo** | Once per run: survive a killing blow at 1 HP |
| **Broken Watch** | Doubles dice roll once per loop — also doubles enemy count |
| **Lighter** | Burn enemies on hit, chance to ignite tile for chain damage |
| **Father's Ring** | Gain Vitality each loop survived |
| **Cracked Radio** | Reveal nearby Story Tiles on the board |
| **Rusty Syringe** | Cannibalize activates even against already-dead enemies |

Remnants are bought with Blood Samples or found on rare Story Tile outcomes.

---

## 12. Story Tiles

Narrated text moments that build the world. Each has a **choice** with consequence.

Examples:
- *"You find a child's drawing on the floor. A family. A house. A red sky."* → Gain a Remnant, or lose Infection resistance
- *"A radio crackles. Someone is broadcasting. You write down the coordinates."* → Unlocks a new tile type next run
- *"A locked door. You hear breathing inside."* → Open it (risk/reward) or leave it
- *"A soldier's journal. Last entry dated three weeks ago."* → Pure lore, small Scrap reward

Story Tile choices across multiple runs build toward **meta-unlocks** — new map areas, new enemies, new Remnants, deeper lore.

---

## 13. Boss Encounters

One boss per run, encountered after enough loops. Each has a horror archetype.

| Boss | Description |
|---|---|
| **The Warden** | Armored riot cop turned monster — high Fortitude, slow |
| **The Hive Mother** | Giant Bloater that spawns Crawlers mid-fight |
| **Patient Zero** | The original infected — fast, unpredictable, causes Infection on hit |
| **The Signal** | Tied to the radio Story Tile mystery — unlocked through narrative |

Defeating a boss ends the run with a Victory screen and bonus Blood Samples.

---

## 14. Loop Progression

| Loop | Changes |
|---|---|
| 1–2 | Standard board, learn the basics |
| 3–4 | Shop opens at loop start — buy Remnants with Scrap |
| 5+ | Infection spreads faster, Horde tiles appear, boss unlocks |
| Every loop | Enemies scale up, reward quality improves |

Special Remnants unlock after loop 3–4:
- Deal damage when rolling specific numbers
- Heal on kill
- Double roll effects
- Conditional stat buffs

---

## 15. Meta-Progression — The Bunker

Between runs, the player is in a **Bunker** — a base upgraded with scavenged materials.

| Room | Unlock Effect |
|---|---|
| **Med Bay** | Start each run with less base Infection |
| **Armory** | Better loot pool — higher chance of rare gear |
| **Radio Room** | More Story Tiles spawn per run |
| **Generator** | Gain 1 free Scrap per tile moved |
| **Wall Reinforcements** | Boss fights start with a small shield |

Bunker upgrades persist across all runs and cost Blood Samples.

---

## 16. UI / UX Notes

- **Inventory screen** feels like a worn backpack laid open — dirty, grunge aesthetic
- Item descriptions written as **journal entries** (*"My father's knife. Still sharp."*)
- Equipping items plays tactile sounds (zipper, click, scrape)
- Infection meter is always visible — subtle red pulse when high
- Board view is isometric, dark-toned, fog-of-war reveals tiles as you approach
- Stats screen accessible anytime via a small button — shows current build snapshot

---

## 17. Addictive Design Pillars

1. **Dice dopamine** — rolling is satisfying and unpredictable
2. **Build curiosity** — players always wonder what Remnant combo they can find
3. **Story hooks** — narrative tiles make players want to explore to find out what happens
4. **Infection dread** — constant pressure that never lets you feel safe
5. **Bunker loop** — even a failed run feels rewarding (Blood Samples → upgrades)
6. **One more roll** — the board is short enough that players always think they can make it

---

*Document version 1.0 — subject to revision as development progresses.*
