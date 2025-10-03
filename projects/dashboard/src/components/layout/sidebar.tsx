"use client";

import { usePathname } from "next/navigation";
import { useState } from "react";
import { Home, Package, FolderTree, ChevronDown, Menu } from "lucide-react";
import { cn } from "@/lib/utils";
import { useTheme } from "next-themes";
import { Button } from "../ui/button";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "../ui/collapsible";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "../ui/tooltip";
import { useSidebar } from "@/providers/sidebar-provider"; // Importato useSidebar
import { SidebarLink } from "./sidebar-link"; // Importato SidebarLink

export function Sidebar() {
    const { theme } = useTheme();
    const pathname = usePathname();
    const { isCollapsed, toggleCollapse, expandSidebar } = useSidebar();
    const [isProductManagementOpen, setIsProductManagementOpen] =
        useState(true);

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

    return (
        <TooltipProvider delayDuration={0}>
            <div
                className={cn(
                    "flex h-screen flex-col border-r bg-card transition-all duration-300 shadow-xl", // Ombra leggera aggiunta
                    isCollapsed ? "w-20" : "w-64"
                )}
            >
                {/* Logo and Toggle Button */}
                <div
                    className={cn(
                        "flex h-16 items-center",
                        isCollapsed ? "justify-center" : "px-2" // Margini ristretti
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
                        <div className="flex-1 flex items-center justify-center ms-3">
                            <img
                                src={
                                    theme === "dark"
                                        ? "/assets/logo-full-light.svg"
                                        : "/assets/logo-full-dark.svg"
                                }
                                alt="amer"
                            />
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
                                                        ? "gap-0 justify-center h-10 w-10 mx-auto px-0" // Margini ristretti/fissi
                                                        : "w-full gap-3 justify-between px-2 py-2", // Margini ristretti
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
                                                    <FolderTree className="h-5 w-5 shrink-0" />{" "}
                                                    {/* Icona più grande */}
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
            </div>
        </TooltipProvider>
    );
}
