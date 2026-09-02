# Gibberish

A .NET library tool that turns readable Latin text into unreadable gibberish. It applies a sequence of substitution ciphers: shuffle letters, shuffle punctuation, then optionally map the Latin alphabet onto another script (Georgian or Thai).

This is a **fun obfuscation tool**, not encryption. Do not use it to protect secrets.

## What it does

Given a string, Gibberish can:

1. **Change capitalization** — leave as-is, force uppercase, or force lowercase.
2. **Shuffle letters** — remap Latin letters using a random permutation.
3. **Shuffle punctuation** — remap a standard set of punctuation characters.
4. **Replace the character set** — map A–Z / a–z onto Georgian or Thai glyphs so the result looks like another script.

Transforms run in that order. Each step is independently configurable, so you can shuffle letters without touching punctuation, replace the script without shuffling, and so on.

Whitespace, digits, and characters outside the Latin alphabet / configured punctuation set are left unchanged.

### `Random` vs `Shuffle`

Letter and punctuation transforms each have two permutation modes:

| Mode | What gets remapped |
| --- | --- |
| **Random** | The full alphabet (or full punctuation set) is shuffled, then applied as a cipher. Characters that do not appear in the input can still appear in the output. |
| **Shuffle** | Only the distinct characters that actually appear in the input are shuffled among themselves. The output uses the same character inventory as the input. |
| **None** | Skip that transform. |

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Library usage

Add a project reference to `Gibberish`, then call the `ToGibberish()` extension method:

```csharp
using Gibberish;
using Gibberish.Enums;

// Defaults: random letter shuffle, random punctuation shuffle, Georgian replacement
var output = "Hello, World!".ToGibberish();

// Customize each transform
var formatter = new GibberishFormatter
{
    CapitalizationBehavior = CapitalizationBehavior.Lower,
    LetterOrderBehavior = LetterOrderBehavior.Shuffle,
    PunctuationOrderBehavior = PunctuationOrderBehavior.None,
    CharacterSetReplacementBehavior = CharacterSetReplacementBehavior.Thai
};

var thaiGibberish = "Hello, World!".ToGibberish(formatter);
```

`ToGibberish` throws `ArgumentNullException` if the input string is `null`. An empty string is valid and returns empty.

### Default formatter

When you call `ToGibberish()` with no arguments, the library uses:

| Setting | Default |
| --- | --- |
| `LetterOrderBehavior` | `Random` |
| `PunctuationOrderBehavior` | `Random` |
| `CharacterSetReplacementBehavior` | `Georgian` |
| `CapitalizationBehavior` | `None` |

### Formatter options

#### `CapitalizationBehavior`

| Value | Effect |
| --- | --- |
| `None` | Leave casing as written |
| `Upper` | Convert the entire string to uppercase first |
| `Lower` | Convert the entire string to lowercase first |

Applied before letter shuffling, so a later cipher still sees the cased text.

#### `LetterOrderBehavior`

| Value | Effect |
| --- | --- |
| `None` | Leave Latin letters in place |
| `Random` | Shuffle A–Z and a–z independently, then substitute |
| `Shuffle` | Shuffle only the Latin letters present in the input |

Uppercase and lowercase are treated as separate alphabets in `Random` mode.

#### `PunctuationOrderBehavior`

| Value | Effect |
| --- | --- |
| `None` | Leave punctuation in place |
| `Random` | Shuffle the full punctuation set, then substitute |
| `Shuffle` | Shuffle only the punctuation characters present in the input |

The standard punctuation set is:

```
; : , " . ? ' % $ \ / < > * & ^ # @ ! = - _ +
```

#### `CharacterSetReplacementBehavior`

| Value | Effect |
| --- | --- |
| `None` | Keep Latin letters |
| `Thai` | Map the Latin alphabet onto Thai characters (U+0E01–U+0E34) |
| `Georgian` | Map the Latin alphabet onto Georgian characters (U+10A0–U+10C4, U+10D0–U+10DF) |

This runs **last**, so shuffled Latin letters are what get mapped onto the target script.

### Custom character data

The cipher tables come from `ICharacterData`. Pass your own implementation to control alphabets, punctuation, or replacement scripts (useful in tests):

```csharp
var output = input.ToGibberish(formatter, myCharacterData);
```

The built-in `CharacterData` class supplies Latin A–Z / a–z, the punctuation list above, and the Thai / Georgian replacement sets.

## Command-line tool

`GibberishConsole` reads a text file, transforms it, and writes a sibling file named `<original> - gibberish<ext>`. If that path already exists, it appends a number (` - gibberish 1`, ` - gibberish 2`, …).

```bash
# From the repo root
dotnet run --project GibberishConsole -- input.txt

# Also print the result to stdout
dotnet run --project GibberishConsole -- input.txt --render

# Control each transform
dotnet run --project GibberishConsole -- input.txt \
  --capitalization lower \
  --letter shuffle \
  --punctuation none \
  --characterSet thai
```

### Options

| Option | Values | Description |
| --- | --- | --- |
| `<target>` | file path | Input file (required; first argument) |
| `--output` | path | Write to this path instead of the default sibling file |
| `--render` | flag | Also write the transformed text to the console |
| `--capitalization` | `none`, `upper`, `lower` | Casing transform |
| `--letter` | `none`, `random`, `shuffle` | Letter shuffle mode |
| `--punctuation` | `none`, `random`, `shuffle` | Punctuation shuffle mode |
| `--characterSet` | `none`, `thai`, `georgian` | Script replacement |

Show the same help locally:

```bash
dotnet run --project GibberishConsole -- --help
```

Omitted options use the library defaults listed above.