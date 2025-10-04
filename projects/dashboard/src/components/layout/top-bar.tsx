"use client";

import { Bell, Menu, Search } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Breadcrumb } from "./breadcrumb";

interface TopBarProps {
    setMobileOpen: (open: boolean) => void;
    userMenu: React.ReactNode;
}

export function TopBar({ setMobileOpen, userMenu }: TopBarProps) {
    return (
        <header className="flex h-16 items-center gap-4 border-b bg-card px-4 md:px-6 shadow-sm">
            {/* Mobile Header */}
            <div className="flex md:hidden w-full items-center gap-2">
                <Button
                    variant="ghost"
                    size="icon"
                    onClick={() => setMobileOpen(true)}
                >
                    <Menu className="h-6 w-6" />
                    <span className="sr-only">Toggle menu</span>
                </Button>
                <div className="relative flex-1">
                    <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input placeholder="Search..." className="pl-10 w-full" />
                </div>
                {userMenu}
            </div>

            {/* Desktop Header */}
            <div className="hidden md:flex w-full items-center">
                <div className="flex-1">
                    <Breadcrumb />
                </div>
                <div className="flex flex-1 justify-center">
                    <div className="relative w-full max-w-md">
                        <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                        <Input
                            placeholder="Search..."
                            className="pl-10 w-full"
                        />
                    </div>
                </div>
                <div className="flex flex-1 items-center justify-end gap-2">
                    <Button variant="ghost" size="icon" className="relative">
                        <Bell className="h-6 w-6" />
                        <span className="sr-only">Notifications</span>
                    </Button>
                    {userMenu}
                </div>
            </div>
        </header>
    );
}
