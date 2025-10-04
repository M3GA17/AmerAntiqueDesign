// projects/dashboard/src/app/products/page.tsx
"use client";

import * as React from "react";
import { LayoutGrid, List, Table } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip";
import { ProductsDataTable } from "@/components/products/data-table";

export default function ProductsPage() {
    const [view, setView] = React.useState("table");

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">
                        Products
                    </h1>
                    <p className="text-muted-foreground mt-2">
                        Manage your products here
                    </p>
                </div>
                <div className="flex items-center gap-2">
                    <TooltipProvider>
                        <Tooltip>
                            <TooltipTrigger asChild>
                                <Button
                                    variant={
                                        view === "table" ? "secondary" : "ghost"
                                    }
                                    size="icon"
                                    onClick={() => setView("table")}
                                >
                                    <Table className="h-4 w-4" />
                                </Button>
                            </TooltipTrigger>
                            <TooltipContent>
                                <p>Table View</p>
                            </TooltipContent>
                        </Tooltip>
                        <Tooltip>
                            <TooltipTrigger asChild>
                                <Button
                                    variant={
                                        view === "list" ? "secondary" : "ghost"
                                    }
                                    size="icon"
                                    onClick={() => setView("list")}
                                >
                                    <List className="h-4 w-4" />
                                </Button>
                            </TooltipTrigger>
                            <TooltipContent>
                                <p>List View</p>
                            </TooltipContent>
                        </Tooltip>
                        <Tooltip>
                            <TooltipTrigger asChild>
                                <Button
                                    variant={
                                        view === "grid" ? "secondary" : "ghost"
                                    }
                                    size="icon"
                                    onClick={() => setView("grid")}
                                >
                                    <LayoutGrid className="h-4 w-4" />
                                </Button>
                            </TooltipTrigger>
                            <TooltipContent>
                                <p>Grid View</p>
                            </TooltipContent>
                        </Tooltip>
                    </TooltipProvider>
                </div>
            </div>

            {view === "table" && <ProductsDataTable />}
            {view === "list" && (
                <div className="text-center p-8">List view coming soon!</div>
            )}
            {view === "grid" && (
                <div className="text-center p-8">Grid view coming soon!</div>
            )}
        </div>
    );
}
