"use client";

import { Bell, Menu, Search } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Breadcrumb } from "./breadcrumb"; // Import locale

interface TopBarProps {
    setMobileOpen: (open: boolean) => void;
    userMenu: React.ReactNode;
}

export function TopBar({ setMobileOpen, userMenu }: TopBarProps) {
    // Ho impostato una larghezza fissa per le colonne laterali (es. w-[200px] o w-[240px] per desktop più grandi)
    // e flex-1 per la colonna centrale, per garantire il bilanciamento.
    return (
        <header className="flex h-16 items-center gap-4 bg-card px-4 md:px-6 shadow-sm dark:bg-[var(--color-card)]">
            {/* Colonna Sinistra (Menu Mobile + Breadcrumb Desktop) - Larghezza Fissa */}
            {/* Ho impostato w-[200px] che corrisponde circa allo spazio lasciato dal sidebar chiuso (w-20 o 80px) + i margini, per un buon bilanciamento. */}
            <div className="flex items-center w-[160px] lg:w-[200px]">
                {/* Mobile Menu Button */}
                <Button
                    variant="ghost"
                    size="icon"
                    className="md:hidden"
                    onClick={() => setMobileOpen(true)}
                >
                    <Menu className="h-6 w-6" />
                    <span className="sr-only">Toggle menu</span>
                </Button>

                {/* Left Column: Breadcrumb (Desktop) */}
                <div className="hidden md:block">
                    <Breadcrumb />
                </div>
            </div>

            {/* Colonna Centrale (Search Box) - Spazio Rimanente (flex-1) */}
            <div className="hidden md:flex flex-1 justify-center min-w-0 max-w-2xl mx-auto">
                <div className="relative w-full max-w-lg">
                    <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input placeholder="Search..." className="pl-10 w-full" />
                </div>
            </div>

            {/* Colonna Destra (Notifiche & User Menu) - Larghezza Fissa, allineata a destra */}
            <div className="flex items-center gap-2 w-[160px] lg:w-[200px] justify-end">
                {/* Notification Bell */}
                <Button variant="ghost" size="icon" className="relative">
                    <Bell className="h-5 w-5" />
                    <span className="sr-only">Notifications</span>
                </Button>

                {/* User Menu */}
                {userMenu}
            </div>

            {/* Mobile Breadcrumb (Centrato) */}
            <div className="md:hidden absolute left-1/2 -translate-x-1/2">
                <Breadcrumb />
            </div>
        </header>
    );
}
