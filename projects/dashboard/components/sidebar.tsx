"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import { useState } from "react"
import Image from "next/image"
import { 
  Home, 
  Package, 
  FolderTree, 
  ChevronDown, 
  Settings, 
  Moon, 
  Sun, 
  User,
  ChevronLeft,
  ChevronRight,
} from "lucide-react"
import { useTheme } from "next-themes"
import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Card } from "@/components/ui/card"
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip"
import { useSidebar } from "./sidebar-provider"

export function Sidebar() {
  const pathname = usePathname()
  const { theme, setTheme } = useTheme()
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
        {/* Logo */}
        <div className="flex h-16 items-center justify-center border-b px-4">
          <div className="flex items-center gap-2 overflow-hidden">
            {isCollapsed ? (
              <Image 
                src="/logo-square.svg" 
                alt="Logo" 
                width={32} 
                height={32}
                className="text-foreground"
              />
            ) : (
              <div className="flex items-center gap-2">
                <Package className="h-6 w-6 shrink-0" />
                <span className="text-xl font-bold whitespace-nowrap">AmerAntique</span>
              </div>
            )}
          </div>
        </div>

        {/* Toggle Button */}
        <div className="flex items-center justify-end px-4 py-2">
          <Button
            variant="ghost"
            size="icon"
            className="h-8 w-8"
            onClick={toggleCollapse}
          >
            {isCollapsed ? (
              <ChevronRight className="h-4 w-4" />
            ) : (
              <ChevronLeft className="h-4 w-4" />
            )}
          </Button>
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
                      "flex items-center rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                      isCollapsed ? "gap-0 justify-center" : "gap-3",
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
                        "flex w-full items-center rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                        isCollapsed ? "gap-0 justify-center" : "gap-3 justify-between",
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

        {/* Bottom Section */}
        <div className="border-t p-3 space-y-2">
          {/* Settings and Theme Toggle */}
          <div className={cn(
            "flex items-center px-3 py-2",
            isCollapsed ? "justify-center gap-0 flex-col space-y-2" : "justify-between"
          )}>
            <Tooltip>
              <TooltipTrigger asChild>
                <Button
                  variant="ghost"
                  size="icon"
                  className="h-8 w-8"
                  asChild
                >
                  <Link href="/settings">
                    <Settings className="h-4 w-4" />
                  </Link>
                </Button>
              </TooltipTrigger>
              {isCollapsed && (
                <TooltipContent side="right">
                  <p>Settings</p>
                </TooltipContent>
              )}
            </Tooltip>
            <Tooltip>
              <TooltipTrigger asChild>
                <Button
                  variant="ghost"
                  size="icon"
                  className="h-8 w-8"
                  onClick={() => setTheme(theme === "dark" ? "light" : "dark")}
                >
                  <Sun className="h-4 w-4 rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0" />
                  <Moon className="absolute h-4 w-4 rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
                  <span className="sr-only">Toggle theme</span>
                </Button>
              </TooltipTrigger>
              {isCollapsed && (
                <TooltipContent side="right">
                  <p>Toggle theme</p>
                </TooltipContent>
              )}
            </Tooltip>
          </div>

          {/* Profile Card */}
          <Card className={cn("p-3", isCollapsed && "p-2")}>
            <DropdownMenu>
              <Tooltip>
                <TooltipTrigger asChild>
                  <DropdownMenuTrigger asChild>
                    <button className={cn(
                      "flex w-full items-center rounded-lg hover:bg-accent transition-colors",
                      isCollapsed ? "justify-center gap-0" : "gap-3"
                    )}>
                      <Avatar className="h-8 w-8 shrink-0">
                        <AvatarImage src="/avatar.png" alt="User" />
                        <AvatarFallback>
                          <User className="h-4 w-4" />
                        </AvatarFallback>
                      </Avatar>
                      {!isCollapsed && (
                        <div className="flex flex-col items-start text-sm">
                          <span className="font-medium">Admin User</span>
                          <span className="text-xs text-muted-foreground">admin@example.com</span>
                        </div>
                      )}
                    </button>
                  </DropdownMenuTrigger>
                </TooltipTrigger>
                {isCollapsed && (
                  <TooltipContent side="right">
                    <p>Admin User</p>
                  </TooltipContent>
                )}
              </Tooltip>
              <DropdownMenuContent align="end" className="w-56">
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
    </TooltipProvider>
  )
}
