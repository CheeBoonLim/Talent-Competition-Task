import React from 'react'
import NavigationDropdown from './NavigationDropdown.jsx'
import { NavigationLink } from './NavigationDropdown.jsx'
import { NavigationButton } from './NavigationDropdown.jsx'

export default class NavigationBar extends React.Component {
    componentDidMount() {
        $('.nav-secondary .ui.dropdown').dropdown({ on: 'hover' })
    }

    render() {
        const leftTags = this.props.data.map(x => 
            x.type === "link" ? <NavigationLink key={x.label} href={x.href} label={x.label} />
                      : x.type === "button" ? <NavigationButton key={x.label} onClick={x.onClick} />
                      : x.type === "menu" ? < NavigationDropdown key = { x.label } items = { x.items } label = { x.label } />
                      : null
        )
        const rightTags = this.props.rightData == null ? [] : this.props.rightData.map(x =>
            x.type === "link" ? <NavigationLink key={x.label} href={x.href} label={x.label} />
                      : x.type === "button" ? <NavigationButton key={x.label} onClick={x.onClick} label={x.label} />
                      : x.type === "menu" ? <NavigationDropdown key={x.label} items={x.items} label={x.label} />
                      : null
        )
        return (
            <section className="nav-secondary">
                <div className="ui eight item menu">
                    {leftTags}
                    {
                        rightTags.length > 0 && < div className="right item">
                            {rightTags}
                        </div>
                    }
                </div>
            </section>
        )
    }
}