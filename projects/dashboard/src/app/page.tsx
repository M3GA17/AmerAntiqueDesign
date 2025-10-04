"use client";

import * as React from "react";

export default function ProductsPage() {
    const [view, setView] = React.useState("table");

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight">
                        Dashboard
                    </h1>
                    <p className="text-muted-foreground mt-2">
                        Manage your products here
                    </p>
                </div>
            </div>
        </div>
    );
}
