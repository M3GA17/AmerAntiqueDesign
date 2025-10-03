"use client";

import * as React from "react";
import * as SeparatorPrimitive from "@radix-ui/react-separator";

import { cn } from "@/src/lib/utils";

const Separator = React.forwardRef<
    React.ElementRef<typeof SeparatorPrimitive.Root>,
    React.ComponentPropsWithoutRef<typeof SeparatorPrimitive.Root>
>(
    (
        { className, orientation = "horizontal", decorative = true, ...props },
        ref
    ) => (
        <SeparatorPrimitive.Root
            ref={ref}
            decorative={decorative}
            orientation={orientation}
            className={cn(
                "shrink-0 bg-border",
                orientation === "horizontal"
                    ? "h-[1px] w-full"
                    : "h-full w-[1px]",
                className
            )}
            {...props}
        />
    )
);
Separator.displayName = SeparatorPrimitive.Root.displayName;

const NavSeparator = React.forwardRef<
    React.ElementRef<typeof SeparatorPrimitive.Root>,
    React.ComponentPropsWithoutRef<typeof SeparatorPrimitive.Root>
>(
    (
        { className, orientation = "horizontal", decorative = true, ...props },
        ref
    ) => (
        <SeparatorPrimitive.Root
            ref={ref}
            decorative={decorative}
            orientation={orientation}
            className={cn(
                "shrink-0 bg-primary/70", // Colore Primary con opacità ridotta per l'effetto
                orientation === "horizontal"
                    ? "h-[2px] w-full"
                    : "h-full w-[2px]", // Spessore 2px
                className
            )}
            {...props}
        />
    )
);
NavSeparator.displayName = "NavSeparator";

export { Separator, NavSeparator };
