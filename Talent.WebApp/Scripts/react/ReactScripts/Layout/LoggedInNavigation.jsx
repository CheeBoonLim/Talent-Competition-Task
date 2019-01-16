import React from 'react';
import ReactDOM from 'react-dom';
import NavigationBar from './NavigationBar.jsx';

export class LoggedInNavigation extends React.Component {
    constructor(props) {
        super(props);
    };


    render() {

        // Hardcoded values for now, so I can shift this to a (mostly) parameterized design
        // and keep current functionality.
        const data = [
            {
                label: "Dashboard",
                href: "/Account/Profile",
                type: "link"
            },
            {
                label: "Profile",
                href: "/Home/About",
                type: "link"
            },
            {
                label: "Sharing",
                items: [
                    { label: "My Shares", href: "#" },
                    { label: "My Offers", href: "#" },
                    { label: "Sent Offers", href: "#" }
                ]
            }
        ]
        const right = [
            {
                type: 'button',
                label: 'Start Sharing',
                action: function () {
                    alert("testing...")
                }
            }
        ]
        
        return (
            <NavigationBar data={data} rightData={right} />
        )
    };
}