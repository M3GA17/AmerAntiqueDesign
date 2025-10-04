"use client";

import { useSidebar } from "@/providers/sidebar-provider";
import { Menu, Bell, Settings, Moon, Sun, User } from "lucide-react";
import { useTheme } from "next-themes";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { TopBar } from "./top-bar";
import { Sidebar } from "./sidebar";
import { MobileSidebar } from "./mobile-sidebar";

export function DashboardLayout({ children }: { children: React.ReactNode }) {
    const { setMobileOpen } = useSidebar();
    const { theme, setTheme } = useTheme();

    const userMenu = (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button
                    variant="ghost"
                    className="relative h-10 w-10 rounded-full"
                >
                    <Avatar className="h-10 w-10">
                        <AvatarImage src="/assets/avatar.png" alt="User" />
                        <AvatarFallback>
                            <User className="h-5 w-5" />
                        </AvatarFallback>
                    </Avatar>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-56">
                <div className="flex items-center justify-start gap-2 p-2">
                    <div className="flex flex-col space-y-1">
                        <p className="text-sm font-medium">Admin User</p>
                        <p className="text-xs text-muted-foreground">
                            admin@example.com
                        </p>
                    </div>
                </div>
                <DropdownMenuSeparator />
                <DropdownMenuItem>
                    <Settings className="mr-2 h-4 w-4" />
                    Settings
                </DropdownMenuItem>
                <DropdownMenuItem
                    onClick={() =>
                        setTheme(theme === "dark" ? "light" : "dark")
                    }
                >
                    {theme === "dark" ? (
                        <Sun className="mr-2 h-4 w-4" />
                    ) : (
                        <Moon className="mr-2 h-4 w-4" />
                    )}
                    Toggle theme
                </DropdownMenuItem>
                <DropdownMenuSeparator />
                <DropdownMenuItem className="text-destructive">
                    Log out
                </DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    );

    return (
        <div className="flex h-screen overflow-hidden">
            {/* Desktop Sidebar */}
            <aside className="hidden md:block">
                <Sidebar />
            </aside>

            {/* Mobile Sidebar */}
            <MobileSidebar />

            {/* Main Content Area */}
            <div className="flex flex-1 flex-col overflow-hidden">
                {/* Top Bar (Barra Superiore) */}
                <TopBar setMobileOpen={setMobileOpen} userMenu={userMenu} />

                {/* Main Content */}
                <main className="flex-1 overflow-y-auto p-4 md:p-6">
                    {children}
                </main>
            </div>
        </div>
    );
}
