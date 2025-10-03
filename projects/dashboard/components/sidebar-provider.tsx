"use client"

import React, { createContext, useContext, useState, useEffect } from "react"

interface SidebarContextType {
  isCollapsed: boolean
  isMobileOpen: boolean
  toggleCollapse: () => void
  setMobileOpen: (open: boolean) => void
  expandSidebar: () => void
}

const SidebarContext = createContext<SidebarContextType | undefined>(undefined)

export function SidebarProvider({ children }: { children: React.ReactNode }) {
  const [isCollapsed, setIsCollapsed] = useState(false)
  const [isMobileOpen, setIsMobileOpen] = useState(false)

  // Load saved state from localStorage on mount
  useEffect(() => {
    const saved = localStorage.getItem("sidebar-collapsed")
    if (saved !== null) {
      setIsCollapsed(saved === "true")
    }
  }, [])

  const toggleCollapse = () => {
    setIsCollapsed((prev) => {
      const newValue = !prev
      localStorage.setItem("sidebar-collapsed", String(newValue))
      return newValue
    })
  }

  const expandSidebar = () => {
    setIsCollapsed(false)
    localStorage.setItem("sidebar-collapsed", "false")
  }

  const value: SidebarContextType = {
    isCollapsed,
    isMobileOpen,
    toggleCollapse,
    setMobileOpen: setIsMobileOpen,
    expandSidebar,
  }

  return (
    <SidebarContext.Provider value={value}>
      {children}
    </SidebarContext.Provider>
  )
}

export function useSidebar() {
  const context = useContext(SidebarContext)
  if (context === undefined) {
    throw new Error("useSidebar must be used within a SidebarProvider")
  }
  return context
}
