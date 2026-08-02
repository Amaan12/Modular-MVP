# Model-View-Presenter (MVP) UI Framework for Unity

A lightweight, production-ready, decoupled **Model-View-Presenter (MVP)** architecture (Passive View variant) built for Unity uGUI. 

This framework completely decouples gameplay logic (`Health`, `Stamina`, `AudioSettings`) from UI visual presentation (`SliderView`, `TextView`), allowing you to swap UI styles or bind new stats in seconds without modifying existing scripts.

---

## 📦 Installation via Unity Package Manager (Git URL)

To install this package directly into your Unity project's `Packages/` folder:

1. Open your Unity Project.
2. Open the **Package Manager** window (**Window > Package Manager**).
3. Click the **`+`** button in the top-left corner and select **"Add package from git URL..."**.
4. Enter the repository Git URL:
   ```
   https://github.com/<your-username>/<your-repo-name>.git
   ```
5. Click **Add**.

### 📥 Downloading Samples

Once installed, 2 downloadable samples are available directly in Unity Package Manager under the **Samples** tab of this package:
1. **Basic MVP Sample**: Contains `Health.cs`, `IDamageable.cs`, and `MVP.unity` demo scene.
2. **InitArgs Presenters**: Contains plain C# `InitArgs` presenters and initializers (`OneWay` and `TwoWay`).

Click **Import** next to either sample to copy it into your project's `Assets/Samples/` folder.

---

## 📁 Directory Structure & Class Guide

```
Packages/com.modular.mvp/
├── package.json
├── Runtime/
│   ├── Com.Modular.MVP.asmdef
│   ├── Interfaces/
│   │   ├── IReadOnlyStat.cs
│   │   ├── IMutableStat.cs
│   │   ├── IStat.cs
│   │   ├── IView.cs
│   │   └── ITwoWayView.cs
│   ├── Model/
│   │   └── StatRange.cs
│   ├── Presenter/
│   │   └── MonoBehaviours/    (Pure UnityEngine.MonoBehaviour Binders)
│   │       ├── OneWay/
│   │       └── TwoWay/
│   └── View/
│       ├── SliderView.cs
│       └── TextView.cs
└── Samples~/
    ├── BasicMVP/             (Downloadable Sample 1: Scene & Domain Scripts)
    │   ├── IDamageable.cs
    │   ├── Health.cs
    │   └── MVP.unity
    └── InitArgs/             (Downloadable Sample 2: Plain C# Presenters)
        ├── OneWay/
        └── TwoWay/
```

---

### 1. Interfaces (`Interfaces/`)

* **`IReadOnlyStat<T>`**: Read-only contract for UI display. Exposes `event Action<T> OnChanged` and `T Value { get; }`. UI Binders consume this to ensure UI code cannot mutate state.
* **`IMutableStat<T>`**: Write-only contract exposing `void Set(T newValue)`. Used by systems that need to mutate data without subscribing to events.
* **`IStat<T>`**: Full contract inheriting `IReadOnlyStat<T>` and `IMutableStat<T>`. Used by 2-way binders (like settings sliders) or game state managers.
* **`IView<T>`**: 1-Way rendering contract exposing `void Render(T value)`.
* **`ITwoWayView<T>`**: Extends `IView<T>` by adding `event Action<T> OnUserInteracted` for interactive UI components (sliders, input fields).

---

### 2. Models (`Model/` & `Sample/`)

* **`StatRange`**: A lightweight serializable struct holding `Current`, `Max`, and a helper `Normalized` ratio (`Current / Max`). Used for ranged stats like Health, Stamina, and Mana.
* **`IDamageable`**: Sample domain interface extending `IReadOnlyStat<StatRange>`. Gives weapons and hazards safe access to `TakeDamage()` and `Heal()` without exposing full `Set()` mutation.
* **`Health`**: Sample MonoBehaviour implementing `IDamageable` and `IStat<StatRange>`. Emits `StatRange` events whenever health changes.

---

### 3. Presenters (`Presenter/`)

Presenters act as the glue between Models and Views. They auto-subscribe on `OnEnable()` and auto-unsubscribe on `OnDisable()`.

#### Standard Unity MonoBehaviours (`Presenter/MonoBehaviours/`)
* Pure `UnityEngine.MonoBehaviour` classes with zero external framework dependencies.
* Inspector drag-and-drop auto-binding via 1-line subclass binders (`FloatBinderOneWay`, `StatRangeBinderOneWay`, etc.).

#### InitArgs Plain C# Presenters & Initializers (`Samples~/InitArgs/`)
* Plain C# classes (`PresenterOneWay<T>`, `PresenterTwoWay<T>`) implementing InitArgs `IInitializable<T1, T2>` and `IDisposable`.
* Includes InitArgs `WrapperInitializer` components (`FloatPresenterOneWayInitializer`, `StatRangePresenterOneWayInitializer`, etc.) for Inspector drag-and-drop initialization of plain C# presenters.

---

### 4. Views (`View/`)

* **`SliderView`**: Implements `ITwoWayView<float>` and `ITwoWayView<StatRange>`. Renders a UI `Slider` for float values or normalized `StatRange` structs.
* **`TextView`**: Implements `IView<string>`, `IView<int>`, `IView<float>`, and `IView<StatRange>`. Renders text via `TextMeshProUGUI`.
