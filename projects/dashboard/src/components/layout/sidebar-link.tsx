"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import * as React from "react";
import { cn } from "@/lib/utils";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip";

interface SidebarLinkProps extends React.ComponentPropsWithoutRef<typeof Link> {
    href: string;
    icon: React.ElementType;
    label: string;
    isCollapsed: boolean;
    onClick?: () => void;
    children?: React.ReactNode;
}

export function SidebarLink({
    href,
    icon: Icon,
    label,
    isCollapsed,
    onClick,
    className,
    children,
    ...props
}: SidebarLinkProps) {
    const pathname = usePathname();
    const isActive =
        href !== "#" &&
        (pathname === href || (href !== "/" && pathname.startsWith(href)));

    const linkContent = (
        <div
            className={cn(
                "flex items-center h-10 px-3 rounded-lg text-sm font-medium transition-colors",
                isActive
                    ? "bg-primary text-primary-foreground"
                    : "text-muted-foreground hover:bg-accent hover:text-accent-foreground",
                className
            )}
            onClick={onClick}
        >
            <div className="flex items-center gap-4">
                <Icon className="h-6 w-6 shrink-0" />
                <span
                    className={cn(
                        "whitespace-nowrap transition-opacity",
                        isCollapsed && "opacity-0 hidden" // Hide text when collapsed
                    )}
                >
                    {label}
                </span>
            </div>
            <div className="flex-1" />
            {!isCollapsed && children}
        </div>
    );

    if (isCollapsed && href !== "#") {
        return (
            <TooltipProvider delayDuration={0}>
                <Tooltip>
                    <TooltipTrigger asChild>
                        <Link href={href} {...props}>
                            {linkContent}
                        </Link>
                    </TooltipTrigger>
                    <TooltipContent side="right">
                        <p>{label}</p>
                    </TooltipContent>
                </Tooltip>
            </TooltipProvider>
        );
    }

    if (href === "#") {
        return <div className="cursor-pointer w-full">{linkContent}</div>;
    }

    return (
        <Link href={href} {...props}>
            {linkContent}
        </Link>
    );
}
