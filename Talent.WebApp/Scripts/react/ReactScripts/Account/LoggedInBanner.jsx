import React from 'react';
import ReactDOM from 'react-dom';

export class LoggedInBanner extends React.Component {
    constructor(props) {
        super(props);
        this.isUserAuthenticated = this.isUserAuthenticated.bind(this);
        this.state = {
            username: '',
        }
    };
    isUserAuthenticated() {
        var self = this;
        $.ajax({
            url: '/Home/IsUserAuthenticated',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.isAuthenticated) {
                    self.setState({
                        username: res.username
                    })
                }
                else {
                    window.location = '/Home';
                }
            }
        })
    };
    componentDidMount() {
        this.isUserAuthenticated();
    };
    render() {
        return (
            <div>
                <div className="ui secondary menu">
                    <a className="item" href="/">Mars Logo</a>
                    <div className="ui small icon input search-box">
                        <input type="text" placeholder="Search skills"></input>
                        <i className="search icon"></i>
                    </div>
                    <div className="right item">
                        <a className="item">Share Your Skills</a>
                        <a className="item">Notification</a>
                        <a className="item">Hi {this.state.username}</a>
                    </div>
                </div>
            </div>
        )
    }
}
