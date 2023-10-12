using System.Net.Http.Headers;
using System.Reflection;

namespace CGullProject.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Return array of all products in database whose ids match one of the param'd ids. If ids is null,
        /// return entire inventory.
        /// </summary>
        /// <param name="ids"> List of ids of products we want to find. If null return array of ALL items.
        /// </param>
        /// <returns> List of products. If any id in parameterized id string array is malformed, return null
        /// so that controller can return badrequest signal in response to malformed http request. </returns>
        public Product[]? GetProductsById(String[]? ids);

        /// <summary>
        /// Tentative stub. Assumes that only one bundle can be associated with an item. 
        /// Also assuming def of bundle is like Amazon's bundled deals.
        /// </summary>
        /// <param name="id"> Id of product we're trying to find bundles for. 
        /// </param>
        /// <returns></returns>
        public Bundle? GetPotentialBundles(String id);


    }
}
