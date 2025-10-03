"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { useState } from "react";
import {
    Home,
    Package,
    FolderTree,
    ChevronDown,
    Settings,
    Moon,
    Sun,
    User,
} from "lucide-react";
import { useTheme } from "next-themes";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Card } from "@/components/ui/card";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "@/components/ui/collapsible";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
    Sheet,
    SheetContent,
    SheetHeader,
    SheetTitle,
} from "@/components/ui/sheet";
import { useSidebar } from "@/providers/sidebar-provider";

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

    const isActive = (href: string) => {
        if (href === "/") {
            return pathname === "/";
        }
        return pathname.startsWith(href);
    };

    const handleLinkClick = () => {
        setMobileOpen(false);
    };

    return (
        <Sheet open={isMobileOpen} onOpenChange={setMobileOpen}>
            <SheetContent side="left" className="w-[280px] p-0">
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
                            {/* Direct Links */}
                            {navigation.map((item) => (
                                <Link
                                    key={item.name}
                                    href={item.href}
                                    onClick={handleLinkClick}
                                    className={cn(
                                        "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                        isActive(item.href)
                                            ? "bg-primary text-primary-foreground"
                                            : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                    )}
                                >
                                    <item.icon className="h-4 w-4" />
                                    {item.name}
                                </Link>
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
                                            <FolderTree className="h-4 w-4" />
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
                                    <Link
                                        href="/products"
                                        onClick={handleLinkClick}
                                        className={cn(
                                            "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                            isActive("/products")
                                                ? "bg-primary text-primary-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        )}
                                    >
                                        <Package className="h-4 w-4" />
                                        Products
                                    </Link>
                                    <Link
                                        href="/categories"
                                        onClick={handleLinkClick}
                                        className={cn(
                                            "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                                            isActive("/categories")
                                                ? "bg-primary text-primary-foreground"
                                                : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                                        )}
                                    >
                                        <FolderTree className="h-4 w-4" />
                                        Categories
                                    </Link>
                                </CollapsibleContent>
                            </Collapsible>
                        </nav>
                    </div>

                    {/* Bottom Section */}
                    <div className="border-t p-3 space-y-2">
                        {/* Settings and Theme Toggle */}
                        <div className="flex items-center justify-between px-3 py-2">
                            <Button
                                variant="ghost"
                                size="icon"
                                className="h-8 w-8"
                                asChild
                            >
                                <Link
                                    href="/settings"
                                    onClick={handleLinkClick}
                                >
                                    <Settings className="h-4 w-4" />
                                </Link>
                            </Button>
                            <Button
                                variant="ghost"
                                size="icon"
                                className="h-8 w-8"
                                onClick={() =>
                                    setTheme(
                                        theme === "dark" ? "light" : "dark"
                                    )
                                }
                            >
                                <Sun className="h-4 w-4 rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0" />
                                <Moon className="absolute h-4 w-4 rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
                                <span className="sr-only">Toggle theme</span>
                            </Button>
                        </div>

                        {/* Profile Card */}
                        <Card className="p-3">
                            <DropdownMenu>
                                <DropdownMenuTrigger asChild>
                                    <button className="flex w-full items-center gap-3 rounded-lg hover:bg-accent transition-colors">
                                        <Avatar className="h-8 w-8">
                                            <AvatarImage
                                                src="/avatar.png"
                                                alt="User"
                                            />
                                            <AvatarFallback>
                                                <User className="h-4 w-4" />
                                            </AvatarFallback>
                                        </Avatar>
                                        <div className="flex flex-col items-start text-sm">
                                            <span className="font-medium">
                                                Admin User
                                            </span>
                                            <span className="text-xs text-muted-foreground">
                                                admin@example.com
                                            </span>
                                        </div>
                                    </button>
                                </DropdownMenuTrigger>
                                <DropdownMenuContent
                                    align="end"
                                    className="w-56"
                                >
                                    <DropdownMenuItem>
                                        <User className="mr-2 h-4 w-4" />
                                        Profile
                                    </DropdownMenuItem>
                                    <DropdownMenuItem>
                                        <Settings className="mr-2 h-4 w-4" />
                                        Settings
                                    </DropdownMenuItem>
                                    <DropdownMenuItem className="text-destructive">
                                        Log out
                                    </DropdownMenuItem>
                                </DropdownMenuContent>
                            </DropdownMenu>
                        </Card>
                    </div>
                </div>
            </SheetContent>
        </Sheet>
    );
}
