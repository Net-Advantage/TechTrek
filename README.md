# TechTrek

Microsoft CEO `Satya Nadella` made a significant statement regarding the future of `Software as a Service` (SaaS) in relation to `AI Agents`. He said:
> "SaaS applications or biz apps — the notion that business applications exist, that will probably collapse in the agent era. Because if you think about it, they are essentially CRUD databases with a bunch of business logic. (But) the business logic is all going to these AI agents, right? And these agents are going to be multi-repo CRUD, right? So they’re not going to discriminate between what the backend is. They’re going to update multiple databases, and all the logic will be in the AI tier, so to speak. And once the AI tier becomes the place where all the logic is, then people will start replacing the backends...that means the biz app, the logic tier can be orchestrated by AI and AI agents."

`TechTrek` is my initial attempt to build a AI-driven application.

Here is the approach I have taken:

1. Identify Core SaaS Functions based on a Domain Driven Design Approach
	- Listed key functionalities of a typical SaaS application (e.g., data storage, retrieval, user authentication, report generation).
1. Select Semantic Kernel as the AI Agent Framework
	- Since I am a Microsoft Centric developer, I have decided on Semantic Kernel for my Agent plugin development.
	- It is a lightweight, open-source, and cross-platform framework for building AI agents.
	- It provides modularity, flexibility, and security.
1. Agent Development
	- Developed a Semantic Kernel Agent for each core function identified in step 1.
	- Each agent is responsible for a specific function.
	- Each agent is a plugin that can be added to the Semantic Kernel.
1. Plugin Creation for each Domain
	- Developed Semantic Kernel plugins for each function within the domain.
		- Data manipulations (CRUD operations)
		- Validations
		- Transformations
		- Factories
		- Orchestration


## Code Coverage

[Code Coverage Report](https://net-advantage.github.io/TechTrek/coverage/)


