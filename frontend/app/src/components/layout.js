import Navbar from "./navbar";

const Layout = ({ children }) => {
  return (
    <>
      <Navbar />

      <main className="container">{children}</main>
    </>
  );
};

export default Layout;
