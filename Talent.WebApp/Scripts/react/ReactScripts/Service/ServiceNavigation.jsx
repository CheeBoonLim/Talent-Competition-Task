import React from 'react'
import NavigationBar from '../Layout/NavigationBar.jsx'

export default function ServiceNavigation(props) {
    const data = [
        {
            label: "Graphics & Design",
            type: "menu",
            items: [
                { href: "#", label: "Logo Design", type: "link" },
                { href:"#", label: "Book & Album covers", type: "link" },
                { href:"#", label: "Flyers & Brochures", type: "link" },
                { href:"#", label: "Web & Mobile Design", type: "link" },
                { href:"#", label: "Search & Display Marketing", type: "link" }
            ]
        },
        {
            label: "Digital Marketing",
            type: "menu",
            items: [
                { href:"#", label: "Social Media Marketing", type: "link" },
                { href:"#", label: "Content Marketing", type: "link" },
                { href:"#", label: "Video Marketing", type: "link" },
                { href:"#", label: "Email Marketing", type: "link" },
                { href:"#", label: "Search & Display Marketing", type: "link" }
            ]
        },
        {
            label: "Writing & Translation",
            type: "menu",
            items: [
                { href:"#", label: "Resumes & Cover Letters", type: "link" },
                { href:"#", label: "Proof Reading & Editing", type: "link" },
                { href:"#", label: "Translation", type: "link" },
                { href:"#", label: "Creative Writing", type: "link" },
                { href:"#", label: "Business Copywriting", type: "link" }
            ]
        },
        {
            label: "Video & Animation",
            type: "menu",
            items: [
                { href:"#", label: "Promotional Videos", type: "link" },
                { href:"#", label: "Editing & Post Production", type: "link" },
                { href:"#", label: "Lyric & Music Videos", type: "link" },
                { href:"#", label: "Other", type: "link" }
            ]
        },
        {
            label: "Music & Audio",
            type: "menu",
            items: [
                { href:"#", label: "Mixing & Mastering", type: "link" },
                { href:"#", label: "Voice Over", type: "link" },
                { href:"#", label: "Song Writers & Composers", type: "link" },
                { href:"#", label: "Other", type: "link" }
            ]
        },
        {
            label: "Programming & Tech",
            type: "menu",
            items: [
                { href:"#", label: "WordPress", type: "link" },
                { href:"#", label: "Web & Mobile App", type: "link" },
                { href:"#", label: "Data Analysis & Reports", type: "link" },
                { href:"#", label: "QA", type: "link" },
                { href:"#", label: "Databases", type: "link" },
                { href:"#", label: "Other", type: "link" }
            ]
        },
        {
            label: "Business",
            type: "menu",
            items: [
                { href:"#", label: "Business Tips", type: "link" },
                { href:"#", label: "Presentations", type: "link" },
                { href:"#", label: "Market Advice", type: "link" },
                { href:"#", label: "Legal Consulting", type: "link" },
                { href:"#", label: "Financial Consulting", type: "link" },
                { href:"#", label: "Other", type: "link" }
            ]
        },
        {
            label: "Fun & Lifestyle",
            type: "menu",
            items: [
                { href:"#", label: "Online Lessons", type: "link" },
                { href:"#", label: "Relationship Advice", type: "link" },
                { href:"#", label: "Astrology", type: "link" },
                { href:"#", label: "Health, Nutrition & Fitness", type: "link" },
                { href:"#", label: "Gaming", type: "link" },
                { href:"#", label: "Other", type: "link" }
            ]
        }
    ]

    return (
        <NavigationBar data={data} />
    )
}