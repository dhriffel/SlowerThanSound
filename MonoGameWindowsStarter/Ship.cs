﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public class Ship
    {
        // List of all components
        //      this will likely be a different data structure later
        private List<Component> Components;
        // Dictionary for component textures
        private Dictionary<Component.Component_Type, Texture2D> Textures;

        public Ship()
        {

        }

        public void Initialize()
        {
            Components = new List<Component>();
        }

        // Alternative initialization, allows passing in some preset components
        public void Initialize(List<Component> components)
        {
            Initialize();
            Textures = new Dictionary<Component.Component_Type, Texture2D>();

            foreach (Component a in components)
            {
                Components.Add(a);
            }
        }

        // Load the textures for all components on the ship
        public void LoadContent(Dictionary<Component.Component_Type, Texture2D> textures)
        {
            // Loop through each texture that was just passed in and add it to the ship's dictionary
            foreach(KeyValuePair<Component.Component_Type, Texture2D> pair in textures)
            {
                Textures.Add(pair.Key, pair.Value);
            }
            // Loop through all components on the ship and set their textures
            foreach(Component component in Components)
            {
                LoadComponentTexture(component);
            }
        }

        // Loops through all components on the ship and calls Draw() on each one
        public void Draw(SpriteBatch spriteBatch, Grid.GridInfo gridInfo)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
            foreach (Component component in Components)
            {
                // Passes gridInfo so that if/when the grid is altered, the components will be rendered correctly
                component.Draw(spriteBatch, gridInfo);
            }
            spriteBatch.End();
        }

        public void AddComponent(Component component)
        {
            // Load the texture for it since the texture is null by default
            LoadComponentTexture(component);
            Components.Add(component);
        }

        public List<Component> GetComponents()
        {
            return Components;
        }

        // Just looks up the component's texture in the Dictionary. If it's not there, it (currently) throws an error
        private void LoadComponentTexture(Component component)
        {
            if (Textures.ContainsKey(component.ComponentType))
            {
                component.LoadContent(Textures[component.ComponentType]);
            }
            else
            {
                throw new Exception("Component's texture was not found. ComponentType: " + component.ComponentType.ToString());
            }
        }
    }
}
