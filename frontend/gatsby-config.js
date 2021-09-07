module.exports = {
  siteMetadata: {
    siteUrl: "https://www.yourdomain.tld",
    title: "My Gatsby Site",
  },
  plugins: [
    {
      resolve: `gatsby-plugin-create-client-paths`,
      options: { prefixes: [`/app/*`] },
    },
    `gatsby-plugin-react-helmet`,
    {
      resolve: 'gatsby-plugin-load-script',
      options: {
        src: 'https://code.jquery.com/jquery-3.2.1.slim.min.js',
      }
    },
    {
      resolve: 'gatsby-plugin-load-script',
      options: {
        src: 'https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js',

      }
    },
    {
      resolve: 'gatsby-plugin-load-script',
      options: {
        src: 'hhttps://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js',
      }

    },
  ],
};
