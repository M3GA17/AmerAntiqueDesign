"use client";

import { usePathname } from "next/navigation";
import { useState } from "react";
import { Home, Package, FolderTree, ChevronDown } from "lucide-react";
import { useTheme } from "next-themes";
import { cn } from "@/lib/utils";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "../ui/collapsible";
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "../ui/sheet";
import { useSidebar } from "@/providers/sidebar-provider"; // Importato useSidebar
import { SidebarLink } from "./sidebar-link"; // Importato SidebarLink

export function MobileSidebar() {
    const pathname = usePathname();
    const { theme, setTheme } = useTheme();
    const { isMobileOpen, setMobileOpen } = useSidebar();
    const [isProductManagementOpen, setIsProductManagementOpen] =
        useState(true);

    const navigation = [
        {
            name: "Home",
            href: "/",
            icon: Home,
        },
    ];

    const handleLinkClick = () => {
        setMobileOpen(false);
    };

    const isActive = (href: string) => {
        if (href === "/") {
            return pathname === "/";
        }
        return pathname.startsWith(href);
    };

    return (
        <Sheet open={isMobileOpen} onOpenChange={setMobileOpen}>
            <SheetContent
                side="left"
                className="w-[280px] p-0 dark:bg-[var(--color-card)]" // Usa il colore della card (sfondo secondario)
            >
                <div className="flex h-full flex-col">
                    {/* Logo */}
                    <SheetHeader className="border-b p-4">
                        <SheetTitle className="flex items-center gap-2">
                            <Package className="h-6 w-6" />
                            <span className="text-xl font-bold">
                                AmerAntique
                            </span>
                        </SheetTitle>
                    </SheetHeader>

                    {/* Navigation */}
                    <div className="flex-1 overflow-y-auto px-3 py-4">
                        <nav className="space-y-2">
                            {/* Direct Links - Uso SidebarLink e sovrascrivo lo stile per mobile (senza isCollapsed) */}
                            {navigation.map((item) => (
                                <SidebarLink
                                    key={item.name}
                                    href={item.href}
                                    icon={item.icon}
                                    label={item.name}
                                    isCollapsed={false}
                                    onClick={handleLinkClick}
                                    className={cn(
                                        "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                        isActive(item.href)
                                            ? "bg-primary text-primary-foreground"
                                            : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                    )}
                                />
                            ))}

                            {/* Product Management Dropdown */}
                            <Collapsible
                                open={isProductManagementOpen}
                                onOpenChange={setIsProductManagementOpen}
                                className="space-y-2"
                            >
                                <CollapsibleTrigger asChild>
                                    <button
                                        className={cn(
                                            "flex w-full items-center justify-between rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                            pathname.startsWith("/products") ||
                                                pathname.startsWith(
                                                    "/categories"
                                                )
                                                ? "bg-accent text-accent-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        )}
                                    >
                                        <div className="flex items-center gap-3">
                                            <FolderTree className="h-5 w-5" />{" "}
                                            {/* Icona più grande */}
                                            Product Management
                                        </div>
                                        <ChevronDown
                                            className={cn(
                                                "h-4 w-4 transition-transform",
                                                isProductManagementOpen &&
                                                    "rotate-180"
                                            )}
                                        />
                                    </button>
                                </CollapsibleTrigger>
                                <CollapsibleContent className="space-y-1 pl-6">
                                    <SidebarLink
                                        href="/products"
                                        icon={Package}
                                        label="Products"
                                        isCollapsed={false}
                                        onClick={handleLinkClick}
                                        className={cn(
                                            "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                            isActive("/products")
                                                ? "bg-primary text-primary-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        )}
                                    />
                                    <SidebarLink
                                        href="/categories"
                                        icon={FolderTree}
                                        label="Categories"
                                        isCollapsed={false}
                                        onClick={handleLinkClick}
                                        className={cn(
                                            "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                            isActive("/categories")
                                                ? "bg-primary text-primary-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        )}
                                    />
                                </CollapsibleContent>
                            </Collapsible>
                        </nav>
                    </div>
                </div>
            </SheetContent>
        </Sheet>
    );
}
