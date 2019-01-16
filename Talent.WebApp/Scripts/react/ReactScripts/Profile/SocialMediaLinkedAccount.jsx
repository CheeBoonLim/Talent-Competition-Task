/* Social media JSX */
import React from 'react';

export default class SocialMediaLinkedAccount extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="four wide column">
                    <h3 className="alt" data-hint="">Linked Accounts</h3>
                    <div className="tooltip">Linking to online social networks adds credibility to your profile. You may add more than one. Note: Your personal information will not be displayed to the buyer.</div>
                </div>
                <div className="three wide column">
                    <button className="ui facebook button social-media">
                        <i className="facebook icon"></i>
                        Facebook
                        </button>
                </div>
                <div className="three wide column">
                    <button className="ui google plus button social-media">
                        <i className="google plus icon"></i>
                        Google Plus
                        </button>
                </div>
                <div className="three wide column">
                    <button className="ui linkedin button social-media">
                        <i className="linkedin icon"></i>
                        LinkedIn
                        </button>
                </div>
                <div className="three wide column">
                    <button className="ui twitter button social-media">
                        <i className="twitter icon"></i>
                        Twitter
                        </button>
                </div>
            </div>
        )
    }
}
