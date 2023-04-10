import random
from typing import Union

from .abstract_mc_state import AbstractMCState, ActionId


class MCTS:

    rand: random.Random

    root: AbstractMCState

    def __init__(self, seed: Union[int, str], root: AbstractMCState):
        self.rand = random.Random(seed)
        self.root = root

    def do_mcts(self) -> AbstractMCState:
        while True:
            state = self.root
            while not state.is_leaf():
                state = max(state.children.values(), key=lambda s: s.calc_ucb1())
            rolled_out_state = self.rollout(state)
            if rolled_out_state.is_winning():
                return rolled_out_state
            state.propagate_simulation_result(rolled_out_state.calc_terminal_value())
            state.expand()

    def rollout(self, state: AbstractMCState) -> AbstractMCState:
        while not state.is_terminal():
            action = self.rand.choice(state.get_available_actions())
            state = state.apply_action(action)
        return state
