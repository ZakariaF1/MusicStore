# AGENTS.md

Music Store is an ASP.NET Core 2.1 Chinook catalog: **MusicStore** (HTTP API + MySQL) and **MusicStoreWebApp** (MVC UI calling that API).

## Standards

Follow [`docs/engineering-charter.md`](docs/engineering-charter.md) for architecture, DDD, docs, CI/TDD, and UX.

## This repo (right-size)

- Existing layered sketch: `MusicStore.Domain`, repository ports, MySQL adapter, API host, separate web UI.
- **Refactor mode** — characterize with tests before structural moves; no big-bang rewrite.
- Do **not** invent empty `Application`/`Infrastructure` folder trees until real types move there.
- Commands live only in [`README.md`](README.md). Architecture lives only in [`docs/architecture/ARD.md`](docs/architecture/ARD.md).

## Stack map (current)

| Charter layer | Where it lives today |
| ------------- | -------------------- |
| Domain | `MusicStore/MusicStore.Domain` (EF attributes still leak — migrate carefully) |
| Application / use cases | `MusicStore/MusicStore/Services` (same host as presentation — debt) |
| Infrastructure | `MusicStore/ClassLibrary1` (MySQL), optional MsSql project |
| Presentation | `MusicStore/MusicStore` (API), `MusicStoreWebApp/` (UI) |
| Client ACL | `MusicStore/MusicStore.Api` (HTTP client DTOs used by the web app) |

## Do / don't

- **Do** ship changes via **pull request only** — never push commits directly to `master`/`main`.
- **Do** keep CI green on PRs; add a failing characterization test before behavior changes.
- **Do** update README commands and ARD (incl. Security Concerns + diagram images) when entry points, deployment, or trust boundaries change.
- **Don't** commit Telerik NuGet credentials or secrets.
- **Don't** put business rules in controllers or Razor views.
- **Don't** push or merge to trunk without a PR (CI runs on PRs only).
