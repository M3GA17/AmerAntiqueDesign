"use client";

import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import { Home, Package, FolderTree, ChevronDown, Menu } from "lucide-react";
import { useTheme } from "next-themes";
import { cn } from "@/lib/utils";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "../ui/collapsible";
import { Sheet, SheetContent } from "../ui/sheet";
import { useSidebar } from "@/providers/sidebar-provider";
import { SidebarLink } from "./sidebar-link";
import { Button } from "../ui/button";

export function MobileSidebar() {
    const pathname = usePathname();
    const { theme } = useTheme();
    const { isMobileOpen, setMobileOpen } = useSidebar();
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

    const handleLinkClick = () => {
        setMobileOpen(false);
    };

    const isActive = (href: string) => {
        if (href === "/") {
            return pathname === "/";
        }
        return pathname.startsWith(href);
    };

    const logoSrc =
        theme === "dark" ? "/assets/logo-light.svg" : "/assets/logo-dark.svg";

    return (
        <Sheet open={isMobileOpen} onOpenChange={setMobileOpen}>
            <SheetContent
                side="left"
                className="w-full p-0 dark:bg-[var(--color-card)] sm:w-[300px]"
                hideCloseButton={true}
            >
                <div className="flex h-full flex-col">
                    {/* Header with Logo and Close Button */}
                    <div className="flex h-16 items-center border-b px-4">
                        <Button
                            variant="ghost"
                            size="icon"
                            className="h-10 w-10 shrink-0"
                            onClick={() => setMobileOpen(false)}
                        >
                            <Menu className="h-6 w-6" />
                            <span className="sr-only">Close Sidebar</span>
                        </Button>
                        <div className="flex-1 flex items-center justify-center">
                            {mounted && (
                                <img
                                    src={logoSrc}
                                    alt="AmerAntique Design"
                                    className="h-8"
                                />
                            )}
                        </div>
                        {/* Spacer to balance the close button */}
                        <div className="w-10" />
                    </div>

                    {/* Navigation */}
                    <div className="flex-1 overflow-y-auto px-3 py-4">
                        <nav className="space-y-2">
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
                                            <FolderTree className="h-6 w-6" />
                                            Product Management
                                        </div>
                                        <ChevronDown
                                            className={cn(
                                                "h-5 w-5 transition-transform",
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
