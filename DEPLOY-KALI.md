# Kali / Portainer deployment

This repository currently serves Cytech Engineering at the root and retains the Techninxa IT & IoT portfolio. The application uses .NET 10, SQL Server EF migrations and SignalR. Do not replace SQL Server with MariaDB without a separate tested migration project.

## First installation (Kali SSH)
Ensure Docker Compose v2, at least 2 GB available for SQL Server alone, sufficient disk and an unused host port (default 8105). The selected Docker subnet must not overlap an existing network. The SQL Server Express edition has capacity limits; verify licensing and sizing for production.

```sh
git clone https://github.com/techninxa2026-design/techninxa_web.git
cd techninxa_web
git checkout release/kali-production
cp .env.example .env
chmod 600 .env
nano .env
docker compose config -q
docker compose build app
docker compose up -d --wait db
```

Replace both password placeholders with the same strong randomly generated password for the first database migration; set the real DNS names in ALLOWED_HOSTS. Do not paste secrets into GitHub or chat. For an existing database, restore and validate a backup before migrating; copy existing uploads into the app-uploads volume separately. Never run down -v on production.

```sh
read -r -p 'Admin email: ' BootstrapAdmin__Email
read -r -s -p 'New admin password (14+ characters): ' BootstrapAdmin__Password; echo
export BootstrapAdmin__Email BootstrapAdmin__Password
docker compose run --rm -e BootstrapAdmin__Email -e BootstrapAdmin__Password app --migrate
unset BootstrapAdmin__Email BootstrapAdmin__Password
docker compose up -d app
curl -fsS http://127.0.0.1:8105/health
curl -fsS http://127.0.0.1:8105/health/ready
```

Legacy plaintext admin credentials are deliberately rejected. The migration command can create or reset one named account with ASP.NET Identity password hashing. Reset any remaining legacy accounts using the same command. For an existing database inspect duplicates first. Use a separate limited SQL login for normal application traffic after initial migration (grant read/write on this database only); update APP_CONNECTION_STRING and recreate app. Use privileged credentials only for a one-off migration. SQL certificate validation is relaxed only for the private container connection; use a trusted certificate if DB is remote.

## Cloudflare / Portainer
Use the existing host-level cloudflared connector. Configure the public hostname (example dotnet.techninxa.com) with service HTTP http://127.0.0.1:8105. Enable HTTPS redirects at Cloudflare. The application trusts forwarded headers only from the configured Docker gateway. Check forwarded HTTPS and login over the public URL, not raw HTTP. If the tunnel is containerized rather than host-level, this loopback origin will not work; attach a suitable private network and explicitly configure that proxy's IP. No database port is published. Portainer can inspect the Compose containers, logs and named volumes; do not deploy a second copy of the same stack. For a Git-backed Portainer deployment use the same compose file, set variables securely, and run the migration command first.

## Release checks and updates
- Check GitHub Actions build and smoke test against a clean disposable SQL database before merge.
- Check the actual public domain and all interactive forms, admin actions, SignalR chat and uploads in a browser. Configure CHAT_USERNAME and CHAT_PASSWORD only if company chat is needed; keep them private.
- Confirm third-party product-image reuse permission and availability, public company information and placeholder content before publication.
- Back up SQL Server via native BACKUP DATABASE and securely copy persistent upload and key volumes; test restoration. Ensure key storage permissions remain private.
- Build a new image and run the one-off migration only after a backup; use versioned image tags in production and keep the prior image available. Rollback of incompatible schema requires restoring a tested backup.
- Monitor app and DB with Beszel/Portainer; alert on disk usage, restart loops and HTTP availability.

No live Kali machine was accessed during preparation. Local tests in CI do not prove public DNS, SSL, browser functionality or production-data compatibility.
