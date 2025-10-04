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
                    "flex h-screen flex-col bg-card transition-all duration-300 shadow-xl relative overflow-x-hidden",
                    isCollapsed ? "w-auto" : "w-[264px]"
                )}
            >
                <div className={cn("flex h-16 items-center shrink-0 px-3")}>
                    <Button
                        variant="ghost"
                        size="icon"
                        className="h-10 w-10 shrink-0"
                        onClick={toggleCollapse}
                    >
                        <Menu className="h-8 w-8" />
                        <span className="sr-only">Toggle Sidebar</span>
                    </Button>
                    {!isCollapsed && (
                        <div className="flex-1 flex items-center justify-center ml-4">
                            {mounted && (
                                <img
                                    src={logoSrc}
                                    alt="AmerAntique Design"
                                    className="h-8"
                                />
                            )}
                        </div>
                    )}
                </div>

                <div className="flex-1 overflow-y-auto p-2">
                    <nav className="space-y-2">
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
                            open={!isCollapsed && isProductManagementOpen}
                            onOpenChange={setIsProductManagementOpen}
                            className="space-y-1"
                        >
                            <TooltipProvider delayDuration={0}>
                                <Tooltip>
                                    <TooltipTrigger asChild>
                                        <div
                                            className={cn(
                                                "flex items-center rounded-lg text-sm font-medium transition-colors cursor-pointer",
                                                (pathname.startsWith(
                                                    "/products"
                                                ) ||
                                                    pathname.startsWith(
                                                        "/categories"
                                                    )) &&
                                                    !isCollapsed
                                                    ? "bg-accent text-accent-foreground"
                                                    : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                            )}
                                        >
                                            <CollapsibleTrigger asChild>
                                                <div className="flex items-center w-full">
                                                    <SidebarLink
                                                        href="#"
                                                        icon={FolderTree}
                                                        label="Product Management"
                                                        isCollapsed={
                                                            isCollapsed
                                                        }
                                                        onClick={
                                                            handleSubmenuClick
                                                        }
                                                        className="w-full justify-between"
                                                    >
                                                        {!isCollapsed && (
                                                            <ChevronDown
                                                                className={cn(
                                                                    "h-5 w-5 transition-transform shrink-0",
                                                                    isProductManagementOpen &&
                                                                        "rotate-180"
                                                                )}
                                                            />
                                                        )}
                                                    </SidebarLink>
                                                </div>
                                            </CollapsibleTrigger>
                                        </div>
                                    </TooltipTrigger>
                                    {isCollapsed && (
                                        <TooltipContent side="right">
                                            <p>Product Management</p>
                                        </TooltipContent>
                                    )}
                                </Tooltip>
                            </TooltipProvider>

                            <CollapsibleContent className="space-y-1 pl-8">
                                <SidebarLink
                                    href="/products"
                                    icon={Package}
                                    label="Products"
                                    isCollapsed={isCollapsed}
                                />
                                <SidebarLink
                                    href="/categories"
                                    icon={FolderTree}
                                    label="Categories"
                                    isCollapsed={isCollapsed}
                                />
                            </CollapsibleContent>
                        </Collapsible>
                    </nav>
                </div>
                <NavSeparator
                    orientation="vertical"
                    className="absolute right-0 top-0 h-full w-[1px]"
                />
            </div>
        </TooltipProvider>
    );
}
