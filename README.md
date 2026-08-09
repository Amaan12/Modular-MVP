# Model-View-Presenter (MVP) UI Framework for Unity

A lightweight, production-ready, decoupled **Model-View-Presenter (MVP)** architecture (Passive View variant) built for Unity uGUI. 

This framework completely decouples gameplay logic (`Health`, `Stamina`, `AudioSettings`) from UI visual presentation (`SliderView`, `TextView`), allowing you to swap UI styles or bind new stats in seconds without modifying existing scripts.

---

### 💡 Why MVP?

In traditional Unity development, whenever a UI component needs to react to a value (like health in `Health.cs`), developers often create a custom script like `HealthUI.cs`. That script listens to events from `Health.cs` and updates the health bar directly.

However, this approach is **not reusable**:
* If you want to use the exact same slider logic (with tweens, animations, or custom formatting) for **Stamina** or **Mana**, you would have to write duplicate scripts (`StaminaUI.cs`, `ManaUI.cs`, etc.).

By using the MVP architecture, we separate the model (e.g., health) from the UI (e.g., the slider). This way, you can use the same slider UI for anything else, like stamina.

---

## 📦 Installation via Unity Package Manager (Git URL)

> [!IMPORTANT]
> Make sure **Git** is pre-installed on your system and added to your `PATH` before proceeding. Unity Package Manager requires Git to fetch packages via Git URL.

To install this package directly into your Unity project's `Packages/` folder:

1. Open your Unity Project.
2. Open the **Package Manager** window (**Window > Package Manager**).
3. Click the **`+`** button in the top-left corner and select **"Add package from git URL..."**.
4. Enter the repository Git URL:
   ```
   https://github.com/Amaan12/Modular-MVP.git
   ```
5. Click **Add**.

### 📥 Downloading Samples

Once installed, 4 downloadable samples are available directly in Unity Package Manager under the **Samples** tab of this package:
1. **Basic MVP Sample**: Basic MVP UI setup with scenes, prefabs, and domain scripts (`Health.cs`, `HealthChanger.cs`).
2. **DOTween MVP Sample**: DOTween animated UI views (punch, shake, rotate, color flash, pop) with demo scene, prefabs, and scripts.
3. **InitArgs Presenters**: Plain C# binders and initializers (`OneWay` and `TwoWay`) for **InitArgs** (a dependency injection asset available on the Unity Asset Store).
4. **Recyclable Scroll View MVP**: Optimized list and grid view sample integrating [Recyclable Scroll Rect - Optimized List/Grid View](https://assetstore.unity.com/packages/tools/gui/recyclable-scroll-rect-optimized-list-grid-view-178560) with Modular MVP architecture.

Click **Import** next to any sample to copy it into your project's `Assets/Samples/` folder.

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
│   │   ├── MonoBehaviours/    (Pure UnityEngine.MonoBehaviour Binders)
│   │   │   ├── OneWay/
│   │   │   └── TwoWay/
│   │   └── RegularClasses/    (Plain C# Binders: ActionPresenter, BinderOneWay, BinderTwoWay)
│   └── View/
│       ├── SliderView.cs
│       └── TextView.cs
└── Samples~/
    ├── BasicMVP/             (Downloadable Sample 1: Basic MVP Setup with Scene & Prefabs)
    ├── DOTweenMVP/           (Downloadable Sample 2: DOTween Animated Views & Demo Scene)
    ├── InitArgs/             (Downloadable Sample 3: Plain C# Binders)
    └── RecyclableScrollViewMVP/ (Downloadable Sample 4: Recyclable Scroll View MVP Sample)
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

* **`StatRange`**: A lightweight serializable struct holding `Current`, `Max`, and `Min` values with a helper `Normalized` ratio (`(Current - Min) / (Max - Min)`). Used for ranged stats like Health, Stamina, and Mana.
* **`IDamageable`**: Sample domain interface extending `IReadOnlyStat<StatRange>`. Gives weapons and hazards safe access to `TakeDamage()` and `Heal()` without exposing full `Set()` mutation.
* **`Health`**: Sample MonoBehaviour implementing `IDamageable` and `IStat<StatRange>`. Emits `StatRange` events whenever health changes.

---

### 3. Binders / Presenters (`Presenter/`)

Binders act as the glue between Models and Views. They auto-subscribe on enable and auto-unsubscribe on disable.

#### Plain C# Regular Classes (`Presenter/RegularClasses/`)
* **`ActionPresenter<T>`**: Plain C# presenter for 1-way event binding directly from actions/events without requiring `IStat`.
* **`BinderOneWay<T>`**: Plain C# 1-way binder connecting `IReadOnlyStat<T>` to `IView<T>` using constructor injection and implementing `IDisposable`.
* **`BinderTwoWay<T>`**: Plain C# 2-way binder connecting `IStat<T>` to `ITwoWayView<T>`.

#### InitArgs Initializers (`Samples~/InitArgs/`)
* Integration for **InitArgs**—a dependency injection framework available on the Unity Asset Store.
* Includes InitArgs `WrapperInitializer` binder components (`FloatBinderOneWayInitializer`, `StatRangeBinderOneWayInitializer`, etc.) for Inspector drag-and-drop initialization of plain C# binders.

#### Recyclable Scroll View Sample (`Samples~/RecyclableScrollViewMVP/`)
* Integration sample demonstrating how to bind recycled list cell item data using [Recyclable Scroll Rect - Optimized List/Grid View](https://assetstore.unity.com/packages/tools/gui/recyclable-scroll-rect-optimized-list-grid-view-178560).
* Includes `RecyclableListPresenter` for binding a data list to recyclerview adapter callbacks and updating item cell views efficiently.

---

### 4. Views (`View/`)

* **`SliderView`**: Implements `ITwoWayView<float>` and `ITwoWayView<StatRange>`. Renders a UI `Slider` for float values or normalized `StatRange` structs.
* **`TextView`**: Implements `IView<string>`, `IView<int>`, `IView<float>`, and `IView<StatRange>`. Renders text via `TextMeshProUGUI`.
