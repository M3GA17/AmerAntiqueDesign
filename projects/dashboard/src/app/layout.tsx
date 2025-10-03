import type { Metadata } from "next";
import "./globals.css";
import { ThemeProvider } from "@/providers/theme-provider";
import { SidebarProvider } from "@/providers/sidebar-provider";
import { DashboardLayout } from "@/components/layout/dashboard-layout";

export const metadata: Metadata = {
    title: "AmerAntique Dashboard",
    description: "Dashboard for antique and modern furniture store",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en" suppressHydrationWarning>
            <body className="antialiased">
                <ThemeProvider
                    attribute="class"
                    defaultTheme="system"
                    enableSystem
                    disableTransitionOnChange
                >
                    <SidebarProvider>
                        <DashboardLayout>{children}</DashboardLayout>
                    </SidebarProvider>
                </ThemeProvider>
            </body>
        </html>
    );
}
