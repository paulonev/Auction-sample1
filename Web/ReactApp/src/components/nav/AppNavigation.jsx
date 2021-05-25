// import _ from "./AppNavigation"; add css somehow
import { Container, Nav, Navbar, NavDropdown, NavItem, Dropdown, NavLink } from "react-bootstrap";
import { useAuth } from "../../utils/authLib";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons";
import { history } from "../..";

export const AppNavigation = () => {
  // const appAuth = useAuth();
  const appAuth = {
    user: {
      isAdmin: false
    },
    signout: function () { }
  }

  //eslint-plugin-react-hooks + styling(for 29-30.05)
  return (
    <Navbar className="header__navigation" expand="sm">
      <Container>
        <Navbar.Brand href="/" className="header__logo">
          <span id="logo-prefix">AU</span>Soft
        </Navbar.Brand>
        <Navbar.Collapse>
          <Nav className="mr-auto header__links">
            <Nav.Link onClick={() => history.push("/auctions")} className="header__link">Auctions</Nav.Link>
            <Nav.Link onClick={() => history.push("/idea")} className="header__link">About</Nav.Link>
            <Nav.Link onClick={() => history.push("/support")} className="header__link">Support</Nav.Link>
          </Nav>
        </Navbar.Collapse>
        {appAuth.user ? (
          <NavDropdown
            title={<FontAwesomeIcon icon={faUser} />}
            style={{ textColor: "#fff", fontSize: "1.3rem" }}
            id="basic-nav-dropdown"
          >
            {appAuth.user.isAdmin ? (
              <NavDropdown.Item
                onClick={() => history.push("/admin")}
              >
                Admin
              </NavDropdown.Item>
            ) : (
                ""
              )}
            <NavDropdown.Item>Profile</NavDropdown.Item>
            <NavDropdown title="Create" id="basic-nav-dropdown">
              <NavDropdown.Item onClick={() => history.push("/auction-create")}><FontAwesomeIcon icon={faUser} /> Auction</NavDropdown.Item>
              <NavDropdown.Item onClick={() => history.push("/slot-create")}><FontAwesomeIcon icon={faUser} /> Slot</NavDropdown.Item>
            </NavDropdown>
            <NavDropdown.Divider />
            <NavDropdown.Item onClick={appAuth.signout()}>Log out</NavDropdown.Item>
          </NavDropdown>
        ) : (
            <Nav className="header__account">
              <NavItem className="account-link">
                <Nav.Link onClick={() => history.push("/sign-in")} className="account-link__login">Log in</Nav.Link>
              </NavItem>
              <NavItem className="account-link">
                <Nav.Link onClick={() => history.push("/sign-up")} className="account-link__register">Sign up</Nav.Link>
              </NavItem>
            </Nav>
          )
        }
      </Container>
    </Navbar>
  );
}