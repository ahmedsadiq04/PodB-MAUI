# .NET MAUI — Lightning Exploration Handout

**Pod:** _____

## Your goal tonight

Get one running screen by the end of class. It doesn't need to be complete or polished — it needs to run. You have most of tonight's 3:45 class session, but this platform has the heaviest install of the night, so setup time matters more here than anywhere else.

## Getting started

- Quickstart: [learn.microsoft.com/en-us/dotnet/maui/get-started/first-app](https://learn.microsoft.com/en-us/dotnet/maui/get-started/first-app)
- Requires Visual Studio 2022 (Windows) with the .NET MAUI workload, or VS Code + the .NET MAUI extension (Mac/Linux). **If this isn't already installed before class starts, do it first and don't wait** — it's the single biggest time risk tonight.
- If the workload install is still going after ~30 minutes, don't keep waiting — switch to reading through the tutorial and sketching your screen in pseudocode/XAML as a group so you're ready to move fast once it's ready.

## Tips for making the most of tonight

- Start with a `ContentPage` containing a `CollectionView` bound to a hardcoded `ObservableCollection` of a simple C# class (mimic Media Tracker's shape — id, title, image). That's your fastest path to something real on screen.
- `CollectionView` is MAUI's rough equivalent of Compose's `LazyColumn`.
- UI lives in XAML markup (a separate file from your C# code-behind), unlike Compose where UI is inline Kotlin — notice how that split changes how you work.
- Try a `{Binding}` expression once you have a list rendering with hardcoded data — that's MAUI's data-binding approach, distinct from Compose's explicit state reads.

## If you finish early

Add a second page and wire navigation with `Shell` navigation. Or replicate one of the actual Media Tracker screens from memory in MAUI.

## Slide Guidelines — Don't Read Your Slides

Your slides are a visual aid for the audience, not a script for you. If a slide has full sentences on it, you'll end up reading them out loud — and after the fourth pod does that in a row, everyone checks out. Part of your presentation grade is the quality of the presentation itself, not just whether the content is correct.

- **6-word rule.** Roughly 6 words per line, 6 lines per slide, max. If your answer needs more than that, that's what you say out loud, not what you type.
- **Fragments, not sentences.** "State management — harder than expected" not "We found that managing state was surprisingly difficult because of X."
- **Show, don't describe.** A screenshot or a short code snippet beats three sentences explaining what the audience could just see.
- **Legible from a distance.** Good habit even on Zoom — if you'd have to shrink the font to fit your text, you have too much text. Cut it, don't shrink it.
- **Test: could you give this talk with the slides turned off?** If the answer is no because you'd forget what to say, that's fine — index cards exist. If the answer is no because the slide *is* the content, rework it.

## Your slide deck (Week 13, 15-minute slot)

One slide per question — 9 slides total:

1. **Title** — pod members, .NET MAUI.
2. **What we built** — screenshot or short screen recording of your running screen.
3. **What was surprisingly easy?**
4. **What was surprisingly hard?**
5. **Would you actually build something in MAUI?**
6. MAUI compiles to real native controls per platform, similar in spirit to React Native or KMP but in C#/XAML. How did the environment setup (Visual Studio, project structure) compare to Compose in Android Studio?
7. XAML markup lives in a separate file from your C# code, unlike Compose where UI is inline Kotlin. How did that split change how you worked?
8. MAUI targets more platforms than almost anything else explored tonight (Windows, macOS, Tizen, plus mobile). Does "write once, run everywhere" feel more real or less real after actually trying it?
9. **What we'd do with more time** — closing thought.

Full grading rubric and question sets: [lightning-exploration-student-guide.md](lightning-exploration-student-guide.md).
