# KickBlast Judo – Training Fee Management System (Student)

## Requirements
- Windows + Visual Studio 2022
- .NET 8 SDK

## Setup
1. Open `KickBlastStudentForms.sln` in Visual Studio.
2. Set `KickBlastStudentForms` as Startup Project.
3. Build and Run (F5).

## Default Login
- Username: `rashmika`
- Password: `123456`

## Features
- Dashboard KPI cards and recent calculations
- Athlete CRUD with search and plan filter
- Monthly fee calculator
- Calculation history with filters and details
- Pricing settings update via `appsettings.json`

## Pricing Edit
- Open **Settings** from sidebar
- Update values and click **Save Settings**
- New pricing is loaded immediately for future calculations

## SQLite Database
- Database file auto-created at runtime: `Data/kickblast_student.db`
- Tables and seed data auto-generated on first run
