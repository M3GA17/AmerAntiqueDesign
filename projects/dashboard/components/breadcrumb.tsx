"use client"

import { usePathname } from "next/navigation"
import Link from "next/link"
import { ChevronRight } from "lucide-react"
import { cn } from "@/lib/utils"

export function Breadcrumb() {
  const pathname = usePathname()

  const pathSegments = pathname.split("/").filter((segment) => segment !== "")

  const breadcrumbs = pathSegments.map((segment, index) => {
    const href = `/${pathSegments.slice(0, index + 1).join("/")}`
    const label = segment.charAt(0).toUpperCase() + segment.slice(1)
    const isLast = index === pathSegments.length - 1

    return {
      href,
      label,
      isLast,
    }
  })

  if (pathname === "/") {
    return (
      <div className="flex items-center gap-2 text-sm">
        <span className="font-medium">Home</span>
      </div>
    )
  }

  return (
    <div className="flex items-center gap-2 text-sm">
      <Link
        href="/"
        className="text-muted-foreground hover:text-foreground transition-colors"
      >
        Home
      </Link>
      {breadcrumbs.map((breadcrumb) => (
        <div key={breadcrumb.href} className="flex items-center gap-2">
          <ChevronRight className="h-4 w-4 text-muted-foreground" />
          {breadcrumb.isLast ? (
            <span className="font-medium">{breadcrumb.label}</span>
          ) : (
            <Link
              href={breadcrumb.href}
              className="text-muted-foreground hover:text-foreground transition-colors"
            >
              {breadcrumb.label}
            </Link>
          )}
        </div>
      ))}
    </div>
  )
}
