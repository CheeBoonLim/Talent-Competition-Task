import React from 'react';

export class Banner extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            searchTerm: ""
        }
        this.search = this.search.bind(this);
        this.handleSearchInputChange = this.handleSearchInputChange.bind(this);
    };
    search() {
        window.location = "/Home/Search?searchTerm" + this.state.searchTerm;
    };
    handleSearchInputChange(event) {
        const value = event.target.value;
        this.setState({
            searchTerm: value
        })
    };
    render() {
        return (
            <div className="first-section">
                <div className="ui secondary menu inverted">
                    <div className="right item">
                        <a className="item">
                            Trade your skill
                        </a>
                        <a className="item" onClick={this.props.signInFunction}>
                            Sign In
                        </a>
                        <button className="ui green basic button" onClick={this.props.signUpFunction}>
                            Join
                    </button>
                    </div>
                </div>
                <div className="tagline-holder">
                    <h1>Trade your skills for a new skill? </h1>
                    <p>Blockchain-based skill exchange platform</p>
                </div>
                <div className="main-search">
                    <div className="big ui action input">
                        <input type="text" placeholder="What skill would you like to trade?" onChange={this.handleSearchInputChange} />
                        <button className=" big ui teal icon button" onClick={this.search}>
                            Search
                </button>
                    </div>
                </div>
            </div>
        )
    }
}

