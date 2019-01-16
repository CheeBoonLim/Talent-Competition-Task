import React from 'react'

export default function NavigationDropdown(props) {
    const tags = props.items.map(x => <NavigationLink key={x.label} href={x.href} label={x.label} />)
    return (
        <div className="ui dropdown link item">
            {props.label}
            <div className="menu">
                {tags}
            </div>
        </div>
    )


}

export function NavigationLink(props) {
    return (
        <a className="item" href={props.href}>{props.label}</a>
    )
}

export function NavigationButton(props) {
    return (
        <button className="ui basic green button" onClick={props.onClick}>{props.label}</button>
    )
}