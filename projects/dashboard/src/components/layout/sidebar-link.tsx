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
    // La proprietà className viene usata per sovrascrivere o aggiungere classi specifiche
}

export function SidebarLink({
    href,
    icon: Icon,
    label,
    isCollapsed,
    onClick,
    className,
    ...props
}: SidebarLinkProps) {
    const pathname = usePathname();
    const isActive =
        pathname === href || (href !== "/" && pathname.startsWith(href));

    const linkContent = (
        <div
            className={cn(
                "flex items-center rounded-lg text-sm font-medium transition-colors cursor-pointer",
                // Margini e padding stretti e coerenti per eliminare l'effetto "jiggle"
                isCollapsed
                    ? "gap-0 justify-center h-10 w-10 mx-auto px-0"
                    : "gap-3 w-full px-2 py-2", // Margine laterale ristretto (px-2)
                isActive
                    ? "bg-primary text-primary-foreground"
                    : "text-muted-foreground hover:bg-accent hover:text-accent-foreground",
                className
            )}
            onClick={onClick}
        >
            <Icon className="h-5 w-5 shrink-0" />{" "}
            {/* Icone leggermente più grandi (h-5 w-5) */}
            {!isCollapsed && <span>{label}</span>}
        </div>
    );

    const linkProps = { href, ...props };

    if (isCollapsed) {
        return (
            <TooltipProvider delayDuration={0}>
                <Tooltip>
                    <TooltipTrigger asChild>
                        <Link {...linkProps}>{linkContent}</Link>
                    </TooltipTrigger>
                    <TooltipContent side="right">
                        <p>{label}</p>
                    </TooltipContent>
                </Tooltip>
            </TooltipProvider>
        );
    }

    return <Link {...linkProps}>{linkContent}</Link>;
}
