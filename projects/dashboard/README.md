# AmerAntique Dashboard

Dashboard for managing antique and modern furniture store built with Next.js, TypeScript, and shadcn/ui.

## Features

- 🎨 **Modern UI** - Clean, professional dashboard layout
- 🌓 **Dark/Light Mode** - Theme toggle with system preference detection
- 📱 **Responsive Design** - Optimized for all screen sizes
- 🧩 **shadcn/ui Components** - Beautiful, accessible UI components
- 🚀 **Next.js 15** - Latest Next.js with App Router and Server Components
- 📝 **TypeScript** - Full type safety
- 🎯 **Tailwind CSS v4** - Modern utility-first CSS

## Layout Structure

### Sidebar Navigation
- Logo at the top
- Direct links (Home)
- Collapsible dropdown menus (Product Management with Products and Categories)
- Settings and theme toggle at the bottom
- User profile card with dropdown menu

### Top Bar
- Breadcrumb navigation showing current path

### Main Content Area
- Responsive content area with pages for Home, Products, Categories, and Settings

## Getting Started

### Prerequisites
- Node.js 20.x or higher
- npm, yarn, or pnpm

### Installation

```bash
# Install dependencies
npm install

# Run the development server
npm run dev
```

Open [http://localhost:3002](http://localhost:3002) with your browser to see the result.

### Build for Production

```bash
# Build the application
npm run build

# Start production server
npm start
```

## Project Structure

```
├── app/
│   ├── layout.tsx          # Root layout with sidebar
│   ├── page.tsx            # Home page
│   ├── products/           # Products management
│   ├── categories/         # Categories management
│   └── settings/           # Settings page
├── components/
│   ├── ui/                 # shadcn/ui components
│   ├── sidebar.tsx         # Main navigation sidebar
│   ├── breadcrumb.tsx      # Breadcrumb navigation
│   └── theme-provider.tsx  # Theme management
└── lib/
    └── utils.ts            # Utility functions
```

## Tech Stack

- **Framework**: Next.js 15.5.4
- **Language**: TypeScript
- **Styling**: Tailwind CSS v4
- **UI Components**: shadcn/ui
- **Icons**: lucide-react
- **Theme**: next-themes

## Configuration

The dashboard runs on port **3002** by default (as configured in package.json).

## Learn More

- [Next.js Documentation](https://nextjs.org/docs)
- [shadcn/ui Documentation](https://ui.shadcn.com)
- [Tailwind CSS Documentation](https://tailwindcss.com/docs)
