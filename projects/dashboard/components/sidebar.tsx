"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import { useState } from "react"
import { 
  Home, 
  Package, 
  FolderTree, 
  ChevronDown, 
  Menu,
} from "lucide-react"
import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible"
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip"
import { useSidebar } from "./sidebar-provider"

export function Sidebar() {
  const pathname = usePathname()
  const { isCollapsed, toggleCollapse, expandSidebar } = useSidebar()
  const [isProductManagementOpen, setIsProductManagementOpen] = useState(true)

  const navigation = [
    {
      name: "Home",
      href: "/",
      icon: Home,
    },
  ]

  const isActive = (href: string) => {
    if (href === "/") {
      return pathname === "/"
    }
    return pathname.startsWith(href)
  }

  const handleSubmenuClick = () => {
    if (isCollapsed) {
      expandSidebar()
    }
    setIsProductManagementOpen(true)
  }

  return (
    <TooltipProvider delayDuration={0}>
      <div 
        className={cn(
          "flex h-screen flex-col border-r bg-card transition-all duration-300",
          isCollapsed ? "w-20" : "w-64"
        )}
      >
        {/* Logo and Toggle Button */}
        <div className={cn(
          "flex h-16 items-center",
          isCollapsed ? "justify-center" : "px-4"
        )}>
          {isCollapsed ? (
            <Button
              variant="ghost"
              size="icon"
              className="h-10 w-10"
              onClick={toggleCollapse}
            >
              <Menu className="h-5 w-5" />
            </Button>
          ) : (
            <>
              <Button
                variant="ghost"
                size="icon"
                className="h-8 w-8 shrink-0"
                onClick={toggleCollapse}
              >
                <Menu className="h-5 w-5" />
              </Button>
              <div className="flex-1 flex items-center justify-center gap-2">
                <Package className="h-6 w-6 shrink-0" />
                <span className="text-xl font-bold whitespace-nowrap">AmerAntique</span>
              </div>
            </>
          )}
        </div>

        {/* Navigation */}
        <div className="flex-1 overflow-y-auto px-3 py-4">
          <nav className="space-y-2">
            {/* Direct Links */}
            {navigation.map((item) => (
              <Tooltip key={item.name}>
                <TooltipTrigger asChild>
                  <Link
                    href={item.href}
                    className={cn(
                      "flex items-center rounded-lg text-sm font-medium transition-colors",
                      isCollapsed ? "gap-0 justify-center h-10 w-10 mx-auto" : "gap-3 px-3 py-2",
                      isActive(item.href)
                        ? "bg-primary text-primary-foreground"
                        : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                    )}
                  >
                    <item.icon className="h-4 w-4 shrink-0" />
                    {!isCollapsed && <span>{item.name}</span>}
                  </Link>
                </TooltipTrigger>
                {isCollapsed && (
                  <TooltipContent side="right">
                    <p>{item.name}</p>
                  </TooltipContent>
                )}
              </Tooltip>
            ))}

            {/* Product Management Dropdown */}
            <Collapsible
              open={isProductManagementOpen}
              onOpenChange={setIsProductManagementOpen}
              className="space-y-2"
            >
              <Tooltip>
                <TooltipTrigger asChild>
                  <CollapsibleTrigger asChild>
                    <button
                      onClick={handleSubmenuClick}
                      className={cn(
                        "flex items-center rounded-lg text-sm font-medium transition-colors",
                        isCollapsed 
                          ? "gap-0 justify-center h-10 w-10 mx-auto" 
                          : "w-full gap-3 justify-between px-3 py-2",
                        (pathname.startsWith("/products") || pathname.startsWith("/categories"))
                          ? "bg-accent text-accent-foreground"
                          : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                      )}
                    >
                      <div className={cn("flex items-center", isCollapsed ? "gap-0" : "gap-3")}>
                        <FolderTree className="h-4 w-4 shrink-0" />
                        {!isCollapsed && <span>Product Management</span>}
                      </div>
                      {!isCollapsed && (
                        <ChevronDown
                          className={cn(
                            "h-4 w-4 transition-transform shrink-0",
                            isProductManagementOpen && "rotate-180"
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
              
              {!isCollapsed && (
                <CollapsibleContent className="space-y-1 pl-6">
                  <Link
                    href="/products"
                    className={cn(
                      "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                      isActive("/products")
                        ? "bg-primary text-primary-foreground"
                        : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                    )}
                  >
                    <Package className="h-4 w-4 shrink-0" />
                    <span>Products</span>
                  </Link>
                  <Link
                    href="/categories"
                    className={cn(
                      "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                      isActive("/categories")
                        ? "bg-primary text-primary-foreground"
                        : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                    )}
                  >
                    <FolderTree className="h-4 w-4 shrink-0" />
                    <span>Categories</span>
                  </Link>
                </CollapsibleContent>
              )}
            </Collapsible>
          </nav>
        </div>


      </div>
    </TooltipProvider>
  )
}
