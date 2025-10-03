// File: projects/dashboard/src/components/layout/sidebar.tsx

"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { Home, Package, FolderTree, ChevronDown, Menu } from "lucide-react";
import { cn } from "@/lib/utils";
import { useTheme } from "next-themes";
import { Button } from "@/components/ui/button";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "@/components/ui/collapsible";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip";
import { useSidebar } from "@/providers/sidebar-provider";
import { SidebarLink } from "./sidebar-link";
import { NavSeparator } from "@/components/ui/separator";

export function Sidebar() {
    const { theme } = useTheme();
    const pathname = usePathname();
    const { isCollapsed, toggleCollapse, expandSidebar } = useSidebar();
    const [isProductManagementOpen, setIsProductManagementOpen] =
        useState(true);

    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
    }, []);

    const navigation = [
        {
            name: "Home",
            href: "/",
            icon: Home,
        },
    ];

    const isActive = (href: string) => {
        if (href === "/") {
            return pathname === "/";
        }
        return pathname.startsWith(href);
    };

    const handleSubmenuClick = () => {
        if (isCollapsed) {
            expandSidebar();
        }
        setIsProductManagementOpen(true);
    };

    const logoSrc =
        theme === "dark" ? "/assets/logo-light.svg" : "/assets/logo-dark.svg";

    return (
        <TooltipProvider delayDuration={0}>
            <div
                className={cn(
                    "flex h-screen flex-col bg-card transition-all duration-300 shadow-xl relative", // Rimosso 'border-r' e aggiunto 'relative'
                    isCollapsed ? "w-20" : "w-64"
                )}
            >
                {/* Logo and Toggle Button */}
                {/* NOTA: Il separatore orizzontale sotto l'intestazione è stato rimosso,
                       quindi l'area del logo si unisce direttamente alla navigazione. */}
                <div
                    className={cn(
                        "flex h-16 items-center",
                        isCollapsed ? "justify-center" : "px-2"
                    )}
                >
                    <Button
                        variant="ghost"
                        size="icon"
                        className="h-10 w-10 shrink-0"
                        onClick={toggleCollapse}
                    >
                        <Menu className="h-6 w-6" />
                        <span className="sr-only">Toggle Sidebar</span>
                    </Button>
                    {!isCollapsed && (
                        <div className="flex-1 flex items-center justify-center ms-7 me-15">
                            {mounted && <img src={logoSrc} alt="amer" />}
                        </div>
                    )}
                </div>

                <div className="flex-1 overflow-y-auto p-2">
                    <nav className="space-y-1">
                        {navigation.map((item) => (
                            <SidebarLink
                                key={item.name}
                                href={item.href}
                                icon={item.icon}
                                label={item.name}
                                isCollapsed={isCollapsed}
                            />
                        ))}

                        <Collapsible
                            open={isProductManagementOpen}
                            onOpenChange={setIsProductManagementOpen}
                            className="space-y-1"
                        >
                            <TooltipProvider delayDuration={0}>
                                <Tooltip>
                                    <TooltipTrigger asChild>
                                        <CollapsibleTrigger asChild>
                                            <button
                                                onClick={handleSubmenuClick}
                                                className={cn(
                                                    "flex items-center rounded-lg text-sm font-medium transition-colors",
                                                    isCollapsed
                                                        ? "gap-0 justify-center h-10 w-10 mx-auto px-0"
                                                        : "w-full gap-3 justify-between px-2 py-2",
                                                    pathname.startsWith(
                                                        "/products"
                                                    ) ||
                                                        pathname.startsWith(
                                                            "/categories"
                                                        )
                                                        ? "bg-accent text-accent-foreground"
                                                        : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                                )}
                                            >
                                                <div
                                                    className={cn(
                                                        "flex items-center",
                                                        isCollapsed
                                                            ? "gap-0"
                                                            : "gap-3"
                                                    )}
                                                >
                                                    <FolderTree className="h-5 w-5 shrink-0" />
                                                    {!isCollapsed && (
                                                        <span className="truncate">
                                                            Product Management
                                                        </span>
                                                    )}
                                                </div>
                                                {!isCollapsed && (
                                                    <ChevronDown
                                                        className={cn(
                                                            "h-4 w-4 transition-transform shrink-0",
                                                            isProductManagementOpen &&
                                                                "rotate-180"
                                                        )}
                                                    />
                                                )}
                                            </button>
                                        </CollapsibleTrigger>
                                    </TooltipTrigger>
                                    {isCollapsed && (
                                        <TooltipContent side="right">
                                            <p>Product Management</p>
                                        </TooltipContent>
                                    )}
                                </Tooltip>
                            </TooltipProvider>

                            {!isCollapsed && (
                                <CollapsibleContent className="space-y-1 pl-6">
                                    <SidebarLink
                                        href="/products"
                                        icon={Package}
                                        label="Products"
                                        isCollapsed={isCollapsed}
                                        className={
                                            isActive("/products")
                                                ? "bg-primary text-primary-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        }
                                    />
                                    <SidebarLink
                                        href="/categories"
                                        icon={FolderTree}
                                        label="Categories"
                                        isCollapsed={isCollapsed}
                                        className={
                                            isActive("/categories")
                                                ? "bg-primary text-primary-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        }
                                    />
                                </CollapsibleContent>
                            )}
                        </Collapsible>
                    </nav>
                </div>
                {/* Separatore Verticale: Agisce come il bordo destro personalizzato */}
                <NavSeparator
                    orientation="vertical"
                    className="absolute right-0 top-0 h-full w-[4px]"
                />
            </div>
        </TooltipProvider>
    );
}
